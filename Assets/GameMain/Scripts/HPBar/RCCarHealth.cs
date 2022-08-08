using System;
using UnityEngine;
using GameFramework.DataTable;

namespace RCToyCar
{
    public class RCCarHealth : MonoBehaviour
    {
        public static float CurrentHealth;
        private bool m_Dead;
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
            if (collision.gameObject.CompareTag("Enemy"))
            {            
                if (PlayerSkill.ShieldActive != 0)
                {
                    PlayerSkill.ShieldActive--;
                }
                else
                {
                    CurrentHealth -= CarAttackDamage;
                }
            }
            if (collision.gameObject.CompareTag("Missile"))
            {
                if (PlayerSkill.ShieldActive != 0)
                {
                    PlayerSkill.ShieldActive--;
                }
                else
                {
                    CurrentHealth -= PlayerSkill.MissileAttackDamage;
                }
            }
            
        }

        public void Recover()
        {
            CurrentHealth += PlayerSkill.HealingHP;
        }

        void OnDeath()
        {
            m_ExplosionParticles.transform.position = transform.position;
            m_ExplosionParticles.gameObject.SetActive(true);
            m_ExplosionParticles.Play();
            gameObject.SetActive(false);
        }

        void CarDataOnLoad()
        {
            IDataTable<DRRCToyCar> dtCarData = GameEntry.DataTable.GetDataTable<DRRCToyCar>();
            DRRCToyCar drCarData = dtCarData.GetDataRow(10000);
            if (drCarData == null)
            {
                return;
            }
            CurrentHealth = drCarData.MaxHP;
            CarAttackDamage = drCarData.CarAttackDamage;
        }
        
    }
}