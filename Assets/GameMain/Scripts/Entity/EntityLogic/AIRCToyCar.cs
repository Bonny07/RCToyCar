using UnityEngine;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using UnityEngine.AI;
using GameFramework.DataTable;

namespace RCToyCar
{
    public class AIRCToyCar : RCTOYCar
    {
        private AIRCToyCarData m_AIRCToyCarData;
        public float patrolWaitTime = 0;
        private static NavMeshAgent s_Agent; //存储游戏进行时敌人小车速度
        private Rigidbody m_Rigidbody;
        private float m_PatrolTimer;
        private int m_RandomNumRange;


        private bool m_IsCrashing = false; //是否处于被撞后的眩晕状态
        private Vector3 WayPointPosition;

        public float Shield; //持有护盾数量
        public float Speedup; //持有加速数量
        public float Missile; //持有导弹数量
        public static float ShieldActive; //护盾是否正在生效
        public GameObject SkillShield;

        private float EnemyRandomSkillPush;
        public static int skillnum; //确保技能仅释放一次
        public static float EnemyShoot; //敌方发射导弹

        private float AddSpeedTimes; //加速倍率
        private float AddSpeedLastTime; //加速持续时间

        private string m_FireButton;
        private bool m_Fired;


        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_Rigidbody = GetComponent<Rigidbody>();
            s_Agent = GetComponent<NavMeshAgent>();
            s_Agent.isStopped = false;
        }
        //初始化小车组件

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            SkillShield = transform.Find("Shield").gameObject;
            m_AIRCToyCarData = userData as AIRCToyCarData;
            if (m_AIRCToyCarData == null)
            {
                Log.Error("AI RCToyCar data is invalid.");
                return;
            }
            s_Agent.speed = m_AIRCToyCarData.Speed;
            s_Agent.destination = m_AIRCToyCarData.WayPointPosition[1];
            Shield = 0;
            Speedup = 0;
            Missile = 0;
        }
        //重置小车组件

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            Patrolling(); //移动
            Skill();
        }

        void Patrolling()
        {
            if (s_Agent.remainingDistance <= s_Agent.stoppingDistance && !m_IsCrashing)
            {
                m_PatrolTimer += Time.deltaTime;
                if (m_PatrolTimer > patrolWaitTime)
                {
                    int n = Random.Range(0, m_AIRCToyCarData.m_RandomNumRange - 1);
                    s_Agent.destination = m_AIRCToyCarData.WayPointPosition[n];
                    m_PatrolTimer = 0;
                }
            }

            if (m_IsCrashing)
            {
                Vector3 movement = Vector3.forward * m_AIRCToyCarData.Speed * (-0.02f);
                m_Rigidbody.transform.position = Vector3.Lerp(m_Rigidbody.position,
                    m_Rigidbody.transform.position + movement, m_AIRCToyCarData.Speed);
            }

            CarWheel();
        }
        //AI移动

        void CarWheel()
        {
            if (!m_IsCrashing)
            {
                GameObject mygameobject1 = GameObject.Find("model_LeftF");
                GameObject mygameobject2 = GameObject.Find("model_LeftB");
                GameObject mygameobject3 = GameObject.Find("model_RightF");
                GameObject mygameobject4 = GameObject.Find("model_RightB");
                mygameobject1.transform.Rotate(Vector3.right, 1 * Time.time);
                mygameobject2.transform.Rotate(Vector3.right, 1 * Time.time);
                mygameobject3.transform.Rotate(Vector3.right, 1 * Time.time);
                mygameobject4.transform.Rotate(Vector3.right, 1 * Time.time);
            }

            if (m_IsCrashing)
            {
                GameObject mygameobject1 = GameObject.Find("model_LeftF");
                GameObject mygameobject2 = GameObject.Find("model_LeftB");
                GameObject mygameobject3 = GameObject.Find("model_RightF");
                GameObject mygameobject4 = GameObject.Find("model_RightB");
                mygameobject1.transform.Rotate(Vector3.right, -1 * Time.time);
                mygameobject2.transform.Rotate(Vector3.right, -1 * Time.time);
                mygameobject3.transform.Rotate(Vector3.right, -1 * Time.time);
                mygameobject4.transform.Rotate(Vector3.right, -1 * Time.time);
            }
        }
        //轮胎滚动效果


        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Wall") && !m_IsCrashing)
            {
                m_IsCrashing = true;
                Invoke("KnockBack", 0.8f);
                GameEntry.Sound.PlaySound(30002);
            }

            if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy")) &&
                !m_IsCrashing)
            {
                if (ShieldActive != 0)
                {
                    ShieldActive--;
                }
                else
                {
                    m_IsCrashing = true;
                    Invoke("KnockBack", 0.8f);
                    GameEntry.Sound.PlaySound(30002);
                    m_AIRCToyCarData.HP -= m_AIRCToyCarData.AttackDamage;
                }
            }

            //伤害计算1

        }

        void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Missile"))
            {
                if (ShieldActive != 0)
                {
                    ShieldActive--;
                }
                else
                {
                    m_AIRCToyCarData.HP -= m_AIRCToyCarData.MisslieDamage;
                }
            }
            //伤害计算2
            
            if (collision.gameObject.CompareTag("HealthPack"))
            {
                GameEntry.Sound.PlaySound(30006);
                m_AIRCToyCarData.HP += m_AIRCToyCarData.Healing;
                Debug.Log("AI拾取工具箱");
            }

            if (collision.gameObject.CompareTag("Shield"))
            {
                GameEntry.Sound.PlaySound(30000);
                Shield=1;
                skillnum = 1;
                Debug.Log("AI拾取护盾");
            }

            if (collision.gameObject.CompareTag("SpeedUp"))
            {
                GameEntry.Sound.PlaySound(30000);
                Speedup=1;
                skillnum = 1;
                Debug.Log("AI拾取加速");
            }

            if (collision.gameObject.CompareTag("MissileProp"))
            {
                GameEntry.Sound.PlaySound(30000);
                Missile=1;
                skillnum = 1;
                Debug.Log("AI拾取导弹");
            }
        }

        void KnockBack()
        {
            m_IsCrashing = false;
        }

        void Skill()
        {
            if (skillnum == 1)
            {
                EnemyRandomSkillPush = Random.Range(1, m_AIRCToyCarData.SkillRange);
                Invoke("Enemyskillpush", EnemyRandomSkillPush);
                skillnum = 0;
            }
            //敌人在随机时间后释放技能
            
            if (ShieldActive != 0f)
            {
                SkillShield.SetActive(true);
            }
            else
            {
                SkillShield.SetActive(false);
            }
            //控制护盾视觉效果
        }

        public void Enemyskillpush()
        {
            if (Shield == 1 && ShieldActive == 0)
            {
                ShieldActive = m_AIRCToyCarData.PreventDamageTime;
                Invoke("ShieldActivating", m_AIRCToyCarData.ShieldLastTime);
                GameEntry.Sound.PlaySound(30003);
                Debug.Log("AI释放护盾");
                Shield=0;
                EnemyRandomSkillPush = Random.Range(1, m_AIRCToyCarData.SkillRange);
            }
            //使用护盾道具

            if (Speedup == 1)
            {
                Invoke("SpeedUp", m_AIRCToyCarData.AddSpeedLastTime);
                GameEntry.Sound.PlaySound(30004);
                s_Agent.speed *= m_AIRCToyCarData.AddSpeed;
                Debug.Log("敌人释放加速");
                Speedup=0;
                EnemyRandomSkillPush = Random.Range(1, m_AIRCToyCarData.SkillRange);
            }
            //使用加速道具

            if (Missile == 1)
            {
                m_Properties.MissilePush();
                GameEntry.Sound.PlaySound(30005);
                Debug.Log("敌人释放导弹");
                Missile=0;
                EnemyRandomSkillPush = Random.Range(1, m_AIRCToyCarData.SkillRange);
            }
            //使用导弹道具
        }
        //AI道具存储统计

        void SpeedUp()
        {
            s_Agent.speed = m_AIRCToyCarData.Speed;
        }
        //加速超出持续时间

        void ShieldActivating()
        {
            if (ShieldActive != 0)
            {
                ShieldActive = 0;
            }
        }
        //护盾超出持续时间

    }
}