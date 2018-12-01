using System;
using System.Collections;
using System.Collections.Generic;
using DogHouse.Core.Services;
using DogHouse.Services;
using UnityEngine;
using UnityEngine.Animations;
using Sirenix.OdinInspector;

namespace DogHouse.Animation
{
    /// <summary>
    /// ChickenAnimationController is a component that 
    /// is an interface for other objects to control 
    /// the chicken's animations.
    /// </summary>
    public class ChickenAnimationController : MonoBehaviour 
    {
        #region Public Variables
        #endregion

        #region Private Variables
        [SerializeField]
        private Animator m_animator;

        [SerializeField]
        private Vector2 m_idleTimeRange;

        private float m_idleTimePassed = 0f;
        private float m_idleThreshHold = 5f;

        private const int IDLE_MIN_INDEX = 0;
        private const int IDLE_MAX_INDEX = 9;
        private const string ANIM_IDLE_INDEX = "IdleIndex";
        #endregion
        
        #region Main Methods
        [Button("Transition To Idle")]
        public void TransitionToIdleAnimation()
        {

        }

        [Button("Transition To Flying")]
        public void TransitionToFlyingAnimation()
        {

        }

        [Button("Transition To Walking")]
        public void TransitionToWalkingAnimation()
        {

        }

        [Button("Transition To Eating")]
        public void TransitionToEatingAnimation()
        {

        }

        private void Update()
        {
            UpdateIdleIndex();
        }

        #endregion

        #region Utility Methods
        private void UpdateIdleIndex()
        {
            m_idleTimePassed += Time.deltaTime;
            if (m_idleTimePassed < m_idleThreshHold) return;
            m_idleTimePassed = 0f;
            m_idleThreshHold = UnityEngine.Random.Range(m_idleTimeRange.x,
                                                        m_idleTimeRange.y);

            SetNewIdleIndex();
        }

        private void SetNewIdleIndex()
        {
            int newIndex = UnityEngine.Random.Range(IDLE_MIN_INDEX,
                                                    IDLE_MAX_INDEX);

            m_animator.SetInteger(ANIM_IDLE_INDEX, newIndex);
        }

        #endregion
    }
}
