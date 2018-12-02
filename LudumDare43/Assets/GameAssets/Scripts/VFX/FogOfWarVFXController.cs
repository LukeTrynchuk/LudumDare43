using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

namespace DogHouse.General
{
    /// <summary>
    /// FogOfWarVFXController is a component that will
    /// control the Fog Of War Effect and allow other
    /// scripts to control at a high level the Fog Of
    /// War effect.
    /// </summary>
    public class FogOfWarVFXController : MonoBehaviour
    {
        #region Public Variables
        public FogOfWarState State => m_state;
        #endregion

        #region Private Variables
        [SerializeField]
        private float m_transitionTime;

        [SerializeField]
        private Renderer m_renderer;

        [SerializeField]
        private FogOfWarState m_state;

        private const string FOG_AMOUNT = "_FogAmount";
        #endregion

        #region Main Methods
        private void OnEnable()
        {
            float amount = DetermineAmount(m_state);
            SetFogAmount(amount);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public void SetState(FogOfWarState NewState)
        {
            if (m_state == NewState || m_state == FogOfWarState.TRANSITIONING)
            {
                return;
            }

            StartCoroutine(TransitionTo(NewState));
        }
        #endregion

        #region Utility Methods
        private void SetFogAmount(float amount)
        {
            m_renderer.material.SetFloat(FOG_AMOUNT, amount);
        }

        private IEnumerator TransitionTo(FogOfWarState NewState)
        {
            float goalAmount = DetermineAmount(NewState);
            float startAmount = DetermineAmount(m_state);
            m_state = FogOfWarState.TRANSITIONING;

            float timePassed = 0f;
            float amount = 0f;

            do
            {
                timePassed += Time.deltaTime;
                amount = Mathf.Clamp01(timePassed / m_transitionTime);
                SetFogAmount(Mathf.Lerp(startAmount, goalAmount, amount));
                yield return null;

            } while (timePassed < m_transitionTime);

            m_state = NewState;
        }

        private float DetermineAmount(FogOfWarState state)
        {
            return (state == FogOfWarState.FULL_FOG) ? 1f : 0f;
        }
        #endregion

        #region Editor Methods
        #if UNITY_EDITOR
        [Button("Full Fog")]
        public void SetToFullFog()
        {
            SetState(FogOfWarState.FULL_FOG);
        }

        [Button("No Fog")]
        public void SetToNoFog()
        {
            SetState(FogOfWarState.ZERO_FOG);
        }
        #endif
        #endregion
    }

    public enum FogOfWarState
    {
        FULL_FOG,
        TRANSITIONING,
        ZERO_FOG
    }
}
