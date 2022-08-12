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
                if (EnemySkill.ShieldActive1 != 0)
                {
                    m_ExplosionParticles.transform.position = transform.position;
                    m_ExplosionParticles.gameObject.SetActive(true);
                    EnemySkill.ShieldActive1--;
                    Invoke("DestroyMissile", 0.05f);
                }
                else
                {
                    m_ExplosionParticles.transform.position = transform.position;
                    m_ExplosionParticles.gameObject.SetActive(true);
                    EnemyHealth.CurrentHealth -= PlayerSkill.MissileAttackDamage;
                    Invoke("DestroyMissile", 0.05f);
                }
            }
            //击中敌方效果
            if (other.gameObject.tag.Equals("Player"))
            {
                if (PlayerSkill.ShieldActive != 0)
                {
                    m_ExplosionParticles.transform.position = transform.position;
                    m_ExplosionParticles.gameObject.SetActive(true);
                    PlayerSkill.ShieldActive--;
                    Invoke("DestroyMissile", 0.05f);
                }
                else
                {
                    m_ExplosionParticles.transform.position = transform.position;
                    m_ExplosionParticles.gameObject.SetActive(true);
                    RCCarHealth.CurrentHealth -= PlayerSkill.MissileAttackDamage;
                    Invoke("DestroyMissile", 0.05f);
                }
            }
            //击中玩家效果

            if (!other.gameObject.tag.Equals("Prop"))
            {
                m_ExplosionParticles.transform.position = transform.position;
                m_ExplosionParticles.gameObject.SetActive(true);
                Invoke("DestroyMissile",0.05f);
            }
        }

        void DestroyMissile()
        {
            m_ExplosionParticles.Play();
            Destroy(gameObject);
        }
    }
}