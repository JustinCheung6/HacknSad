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
        private CharacterController _cController;
        
        private PlayerInput input;
        private InputAction moveInput;

        private void Awake()
        {
            _cController = GetComponent<CharacterController>();
            input = GetComponent<PlayerInput>();

            //Get input actions from PlayerInput
            moveInput = input.actions["Movement"];

        }

        private void OnEnable()
        {
            //Add Methods to input events
            moveInput.performed += OnMove;
        }

        private void OnDisable()
        {
            //Remove Methods from input events
            moveInput.performed -= OnMove;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            try
            {
                
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


        private void FixedUpdate()
        {
            
        }
    }
}
