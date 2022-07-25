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

                if(moveInput != Vector3.zero)
                {
                    controller.Move(Time.fixedDeltaTime * moveSpeed * moveInput);
                    transform.rotation = Quaternion.Euler(0f, UpdateRotation(moveInput.x, moveInput.z), 0f);
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

        private float UpdateRotation(float x, float y)
        {
            if (x == 0f)
            {
                return (y >= 0f) ? 0f : 180f;
            }
            else if (y == 0f)
                return (x > 0f) ? 90f : 270f;


            float angle = Mathf.Atan(Mathf.Abs(y / x)) * 180 / Mathf.PI;

            if (x >= 0 && y >= 0)
                return angle;
            else if (x >= 0 && y < 0)
                return angle + 90;
            else if (x < 0 && y < 0)
                return angle + 180;
            else
                return angle + 270;
        }
    }
}
