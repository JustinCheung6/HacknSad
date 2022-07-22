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
        #endregion

        private Vector3 moveInput = Vector3.zero;

        //Playr Attributes

        [Tooltip("Units per second")]
        [SerializeField] private float moveSpeed = 1f; 

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            pInput = GetComponent<PlayerInput>();

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
                if(moveInput != Vector3.zero)
                {
                    controller.Move(Time.fixedDeltaTime * moveSpeed * moveInput); 
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
    }
}
