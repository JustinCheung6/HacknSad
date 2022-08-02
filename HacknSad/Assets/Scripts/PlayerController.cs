using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        #region Components
        private CharacterController controller;
        private Transform cameraTrans;

        //Player Input
        private PlayerInput pInput;
        private InputAction moveAction;
        private Animator animator;
        #endregion

        #region Enum
        private enum PlayerActions
        {
            Null = -1,
            LtAtk = 0,
            HvyAtk = 1,
            Dodge = 2
        }
        #endregion

        //Inputs
        private Vector3 moveInput = Vector3.zero;
        private PlayerActions nextAction = PlayerActions.Null;

        //Player Attributes
        [Tooltip("Units per second")]
        [SerializeField] private float moveSpeed = 1f;
        [Tooltip("Time before player can make input to continue combo")]
        [SerializeField] private float comboDelay = 0.2f;
        [Tooltip("Whether comboDelay is a flat time in seconds (true), or a % of animation time (false)")]
        [SerializeField] private bool cDelayFlatTime = false;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            pInput = GetComponent<PlayerInput>();
            animator = GetComponentInChildren<Animator>();
            cameraTrans = Camera.main.transform;

            //Get input actions from PlayerInput
            moveAction = pInput.actions["Movement"];

        }

        private void OnEnable()
        {
            //Add Methods to input events
            moveAction.performed += OnMove;
            moveAction.canceled += OnMove;
            UpdateManager.s.OnFixedUpdate += _FixedUpdate;
        }

        private void OnDisable()
        {
            //Remove Methods from input events
            moveAction.performed -= OnMove;
            moveAction.canceled -= OnMove;
            UpdateManager.s.OnFixedUpdate -= _FixedUpdate;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            try
            {
                Vector2 move = context.ReadValue<Vector2>();
                moveInput = new Vector3(move.x, 0, move.y);
            }
            catch (NullReferenceException e)
            {
                Debug.LogException(e);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void _FixedUpdate()
        {
            try
            {
                animator.SetBool("Moving", moveInput != Vector3.zero);

                //Get the forward and right vectors of the camera
                Vector3 forward = new Vector3(cameraTrans.forward.x, 0f, cameraTrans.forward.z).normalized;
                Vector3 right = new Vector3(cameraTrans.right.x, 0f, cameraTrans.right.z).normalized;

                

                //Get move vector relative to camera forward
                Vector3 movement = (forward * moveInput.z) + (right * moveInput.x);

                if (moveInput != Vector3.zero)
                {
                    controller.Move(Time.fixedDeltaTime * moveSpeed * movement);

                    //Debug.Log("Cam: " + forward + " Move: " + movement);

                    float rotation = cameraTrans.rotation.eulerAngles.y + RotationOffset(forward, movement);

                    //Debug.Log(rotation);
                    try
                    {
                        transform.rotation = Quaternion.Euler(0f, rotation, 0f);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Error on: " + rotation);
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Debug.LogException(e);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        //Get angle distance between 2 points (camera forward, player direction)
        private float RotationOffset(Vector3 f, Vector3 p)
        {
            if (f == p)
                return 0;
            else if (-f == p)
                return 180;

            float angleDist = ((f.x * p.x) + (f.z * p.z)) /
                (Mathf.Sqrt(Mathf.Pow(f.x, 2) + Mathf.Pow(f.z, 2)) * Mathf.Sqrt(Mathf.Pow(p.x, 2) + Mathf.Pow(p.z, 2)));

            float angle = Mathf.Acos(angleDist) * Mathf.Rad2Deg;

            float dot = -(f.x * p.z) + (f.z * p.x);

            if(dot < 0)
                angle = -angle;

            //Debug.Log(angle);

            return angle;
        }


        //To Do:
        //Add in Animations and adjust their timings to flow properly (Have anticipation, contact, and follow-through)
        //Have script to adjust their overall times in script
        //Use comboDelay and cDelayFlatTime to adjust comboInputs of the system
    }
}
