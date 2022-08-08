using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RCToyCar
{
    public class MissileExplosion : MonoBehaviour
    {
        private ParticleSystem m_ExplosionParticles;
        public GameObject m_ExplosionPrefab;

        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, PlayerSkill.MissileLastTime);
        }

        private void Awake()
        {
            m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
            m_ExplosionParticles.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            // 如果碰到敌人
            if (other.gameObject.tag.Equals("Enemy"))
            {
                m_ExplosionParticles.transform.position = transform.position;
                m_ExplosionParticles.gameObject.SetActive(true);
                m_ExplosionParticles.Play();
                EnemyHealth.CurrentHealth -= 100;
                other.GetComponent<EnemyHealth>().OnKilled();
                Destroy(gameObject);
            }

            if (!other.gameObject.tag.Equals("Prop"))
            {
                m_ExplosionParticles.transform.position = transform.position;
                m_ExplosionParticles.gameObject.SetActive(true);
                m_ExplosionParticles.Play();
                Destroy(gameObject);
            }
        }
    }
}