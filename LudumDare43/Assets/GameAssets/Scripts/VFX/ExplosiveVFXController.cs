using System.Collections;
using System.Collections.Generic;
using DogHouse.Core.Services;
using DogHouse.Services;
using UnityEngine;
using Sirenix.OdinInspector;

namespace DogHouse.VFX
{
    /// <summary>
    /// ExplosiveVFXController controls the visual aspect 
    /// of the explosive effect.
    /// </summary>
    public class ExplosiveVFXController : MonoBehaviour 
    {
        #region Public Variables
        [Range(0, 1)]
        public float m_explosionAmount;
        #endregion

        #region Private Variables
        [SerializeField]
        private GameObject m_explosionParticlePrefab;

        [SerializeField]
        private Renderer m_graphicRenderer;

        [SerializeField]
        private GameObject m_visualObject;

        [SerializeField]
        private Animator m_animator;

        private EXPLOSION_VFX_STATE m_state = EXPLOSION_VFX_STATE.IDLE;
        #endregion
        
        #region Main Methods
        [Button("Begin Exploding")]
        public void BeginExploding()
        {
            m_state = EXPLOSION_VFX_STATE.EXPLODING;
            m_animator.SetTrigger("EXPLODE");
            Invoke(nameof(GenerateParticles), 5.5f);
            Invoke(nameof(HideVisual), 5.5f);
            Invoke(nameof(FinishExploding), 8f);
        }

        public void HideVisual()
        {
            m_graphicRenderer.enabled = false;
        }

        public void GenerateParticles()
        {
            Debug.Log("GENERATING PARTICLES");
            GameObject vfx = Instantiate(m_explosionParticlePrefab);
            vfx.transform.position = m_visualObject.transform.position;
            vfx.transform.parent = m_visualObject.transform;
        }

        public void FinishExploding()
        {
            m_state = EXPLOSION_VFX_STATE.FINISHED;
        }

        private void Update()
        {
            SetExplosionValue();
        }
        #endregion

        #region Utility Methods
        private void SetExplosionValue()
        {
            if (m_state != EXPLOSION_VFX_STATE.EXPLODING) return;
            m_graphicRenderer.material.SetFloat("_ExplosionAmount", 
                                                m_explosionAmount);
        }
        #endregion
    }

    public enum EXPLOSION_VFX_STATE
    {
        IDLE,
        EXPLODING,
        FINISHED
    }
}
