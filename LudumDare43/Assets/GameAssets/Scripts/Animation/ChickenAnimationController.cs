using UnityEngine;
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
        public ChickenAnimationState State => m_state;
        #endregion

        #region Private Variables
        [SerializeField]
        private Animator m_animator;

        [SerializeField]
        private Vector2 m_idleTimeRange;

        private float m_idleTimePassed = 0f;
        private float m_idleThreshHold = 5f;

        private ChickenAnimationState m_state;

        private const int IDLE_MIN_INDEX = 0;
        private const int IDLE_MAX_INDEX = 9;

        private const string ANIM_IDLE_INDEX = "IdleIndex";
        private const string ANIM_IDLE_BOOL = "Idle";
        private const string ANIM_EATING_BOOL = "Eating";
        private const string ANIM_FLYING_BOOL = "Flying";
        private const string ANIM_WALK_BOOL = "Walk";
        #endregion

        #region Main Methods
        private void Start()
        {
            SetState(ChickenAnimationState.IDLE);
        }

        public void SetChickenAnimationState(ChickenAnimationState state)
        {
            SetState(state);
        }

        [Button("Transition To Idle")]
        private void TransitionToIdleAnimation()
        {
            SetState(ChickenAnimationState.IDLE);
        }

        [Button("Transition To Flying")]
        private void TransitionToFlyingAnimation()
        {
            SetState(ChickenAnimationState.FLY);
        }

        [Button("Transition To Walking")]
        private void TransitionToWalkingAnimation()
        {
            SetState(ChickenAnimationState.WALK);
        }

        [Button("Transition To Eating")]
        private void TransitionToEatingAnimation()
        {
            SetState(ChickenAnimationState.EAT);
        }

        private void Update()
        {
            UpdateIdleIndex();
        }

        #endregion

        #region Utility Methods
        private void UpdateIdleIndex()
        {
            if (m_state != ChickenAnimationState.IDLE) return;

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

        private void SetState(ChickenAnimationState newState)
        {
            bool idle = false; 
            bool walk = false; 
            bool fly = false; 
            bool eat = false;
            if (newState == ChickenAnimationState.EAT) eat = true;
            if (newState == ChickenAnimationState.IDLE) idle = true;
            if (newState == ChickenAnimationState.FLY) fly = true;
            if (newState == ChickenAnimationState.WALK) walk = true;

            SetAnimationStateVariables(idle, walk, fly, eat);
        }

        private void SetAnimationStateVariables(bool Idle, bool Walk, 
                                                bool Fly, bool Eat)
        {
            m_animator.SetBool(ANIM_EATING_BOOL, false);
            m_animator.SetBool(ANIM_IDLE_BOOL, false);
            m_animator.SetBool(ANIM_FLYING_BOOL, false);
            m_animator.SetBool(ANIM_WALK_BOOL, false);

            m_animator.SetBool(ANIM_IDLE_BOOL, Idle);
            m_animator.SetBool(ANIM_WALK_BOOL, Walk);
            m_animator.SetBool(ANIM_FLYING_BOOL, Fly);
            m_animator.SetBool(ANIM_EATING_BOOL, Eat);
        }
        #endregion
    }

    public enum ChickenAnimationState
    {
        IDLE,
        WALK,
        FLY,
        EAT
    }
}
