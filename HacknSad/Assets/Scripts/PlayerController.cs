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

        private Vector3 moveInput = Vector3.zero;

        //Playr Attributes

        [Tooltip("Units per second")]
        [SerializeField] private float moveSpeed = 1f; 

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

                    float rotation = cameraTrans.rotation.eulerAngles.y - RotationOffset(forward, movement);

                    //Debug.Log(rotation);

                    transform.rotation = Quaternion.Euler(0f, rotation, 0f);
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
            //case where player is running forward
            if (f == p)
                return 0f;

            Vector3 relative = cameraTrans.InverseTransformDirection(p);

            float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;

            //float angle = Mathf.Atan2(p.x - f.x, p.z - f.z) * Mathf.Rad2Deg;
            //angle = (angle * 2) + 180;

            Debug.Log(angle);

            return angle;
        }
    }
}
