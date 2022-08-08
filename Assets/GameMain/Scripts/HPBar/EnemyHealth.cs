using System.Collections;
using GameFramework.DataTable;
using UnityEngine;

namespace RCToyCar
{
    public class EnemyHealth : MonoBehaviour
    {
        public static float CurrentHealth;
        private bool m_Dead;
        private Rigidbody m_MissileBody;
        private float CarAttackDamage;

        public GameObject m_ExplosionPrefab;
        private ParticleSystem m_ExplosionParticles;

        private void Awake()
        {
            m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
            m_ExplosionParticles.gameObject.SetActive(false);
        }

        void Start()
        {
            CarDataOnLoad();
        }

        void Update()
        {
            if (CurrentHealth <= 0f)
            {
                OnDeath();
            }
        }
        
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                CurrentHealth -= CarAttackDamage;
            }

            if (collision.gameObject.CompareTag("Missile"))
            {
                CurrentHealth -= PlayerSkill.MissileAttackDamage;
            }
        }

        public void OnKilled()
        {
            OnDeath();
        }

        public void Recover()
        {
            CurrentHealth += PlayerSkill.HealingHP;
        }

        private void OnDeath()
        {
            m_ExplosionParticles.transform.position = transform.position;
            m_ExplosionParticles.gameObject.SetActive(true);
            m_ExplosionParticles.Play();
            gameObject.SetActive(false);
        }

        void CarDataOnLoad()
        {
            IDataTable<DRRCToyCar> dtCarData = GameEntry.DataTable.GetDataTable<DRRCToyCar>();
            DRRCToyCar drCarData = dtCarData.GetDataRow(10001);
            if (drCarData == null)
            {
                return;
            }
            CurrentHealth = drCarData.MaxHP;
            CarAttackDamage = drCarData.CarAttackDamage;
        }

    }
}
