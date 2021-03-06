﻿using System;
using System.Collections;
using System.Collections.Generic;
using DogHouse.Core.Services;
using DogHouse.Services;
using UnityEngine;

namespace DogHouse.General
{
    /// <summary>
    /// GamepadInput is an implementation of
    /// the input service. The gamepad input uses
    /// a game pad for input.
    /// </summary>
    public class GamepadInput : MonoBehaviour, IInputService
    {
        #region Public Variables
        public event Action<Vector2> OnMovementVectorCalculated;
        public event Action OnConfirmButtonPressed;
        public event Action OnDeclineButtonPressed;
        public event Action OnJumpButtonPressed;
        public event Action OnSpawnButtonPressed;
        public event Action<GrabButtonState> OnGrabButtonStateChanged;
        #endregion

        #region Private Variables
        private bool m_jumpButtonDown = false;
        private bool m_spawnButtonDown = false;
        private GrabButtonState m_grabButtonState = GrabButtonState.RELEASED;
        #endregion

        #region Main Methods
        void OnEnable() => RegisterService();
        void OnDisable() => UnregisterService();

        void Update()
        {
            DetermineMovementVector();
            DetermineConfirmButtonPressed();
            DetermineDeclineButtonPressed();
            DetermineJumpButtonPressed();
            DetermineSpawnButtonPressed();
            DetermineGrabButtonPressed();
        }

        public void RegisterService()
        {
            ServiceLocator.Register<IInputService>(this);
        }

        public void UnregisterService()
        {
            ServiceLocator.Unregister<IInputService>(this);
        }
        #endregion

        #region Utility Methods
        private void DetermineMovementVector()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(x, y);

            if (movement.magnitude > 0.2f)
            {
                OnMovementVectorCalculated?.Invoke(movement);
            }
        }

        private void DetermineConfirmButtonPressed()
        {
            float confirmButton = 0f;
            #if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            confirmButton = Input.GetAxis("ConfirmButton_WIN");
            #elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            confirmButton = Input.GetAxis("ConfirmButton_OSX");
            #endif

            if (confirmButton > 05f) OnConfirmButtonPressed?.Invoke();
        }

        private void DetermineDeclineButtonPressed()
        {
            float declineButton = 0f;
            #if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            declineButton = Input.GetAxis("DeclineButton_WIN");
            #elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            declineButton = Input.GetAxis("DeclineButton_OSX");
#endif

            if (declineButton > 05f) OnDeclineButtonPressed?.Invoke();
        }

        private void DetermineJumpButtonPressed()
        {
            float jumpButton = 0f;

            #if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            jumpButton = Input.GetAxis("JumpButton_WIN");
            #elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            jumpButton = Input.GetAxis("JumpButton_OSX");
            #endif

            if(jumpButton > 0.5f)
            {
                if(!m_jumpButtonDown)
                {
                    OnJumpButtonPressed?.Invoke();
                }

                m_jumpButtonDown = true;
                return;
            }

            m_jumpButtonDown = false;
        }

        private void DetermineSpawnButtonPressed()
        {
            float spawnAmount = 0f;

            #if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            spawnAmount = Input.GetAxis("SpawnButton_WIN");
            #elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            spawnAmount = Input.GetAxis("SpawnButton_OSX");
            #endif
        
            if(spawnAmount > 0.5f)
            {
                if(!m_spawnButtonDown)
                {
                    OnSpawnButtonPressed?.Invoke();
                }

                m_spawnButtonDown = true;
                return;
            }

            m_spawnButtonDown = false;
        }

        private void DetermineGrabButtonPressed()
        {
            float grabAmount = 0f;

            #if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            grabAmount = Input.GetAxis("GrabAxis_WIN");
            #elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            grabAmount = Input.GetAxis("GrabAxis_OSX");
            #endif

            if (grabAmount > 0.5f)
            {
                if (m_grabButtonState == GrabButtonState.RELEASED)
                {
                    OnGrabButtonStateChanged?.Invoke(GrabButtonState.PRESSED);
                }

                m_grabButtonState = GrabButtonState.PRESSED;
                return;
            }

            if(grabAmount < 0.5f)
            {
                if(m_grabButtonState == GrabButtonState.PRESSED)
                {
                    OnGrabButtonStateChanged?.Invoke(GrabButtonState.RELEASED);
                }
                m_grabButtonState = GrabButtonState.RELEASED;
                return;
            }
        }
        #endregion
    }
}
