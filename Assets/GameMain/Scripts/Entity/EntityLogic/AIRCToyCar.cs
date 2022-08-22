using UnityEngine;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using UnityEngine.AI;
using GameFramework.DataTable;

namespace RCToyCar
{
    public class AIRCToyCar : RCTOYCar
    {
        [SerializeField]
        private AIRCToyCarData m_AIRCToyCarData;
        public float patrolWaitTime = 0;
        private static NavMeshAgent m_Agent;  //存储游戏进行时敌人小车速度
        private Rigidbody m_Rigidbody;
        private float m_PatrolTimer;
        private int RandomNumRange;
        private float currentHP;
        
        private bool isCrashing=false; //是否处于被撞后的眩晕状态
        private Vector3 WayPointPosition;

        
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_Rigidbody = GetComponent<Rigidbody> ();
            m_Agent = GetComponent<NavMeshAgent>();
            m_Agent.isStopped = false;

        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_AIRCToyCarData = userData as AIRCToyCarData;
            if (m_AIRCToyCarData == null)
            {
                Log.Error("AI RCToyCar data is invalid.");
                return;
            }
            m_Agent.speed= m_AIRCToyCarData.Speed;
            m_Agent.destination = m_AIRCToyCarData.WayPointPosition[1];
        }
        

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            Patrolling(); //移动
        }

        void Patrolling()
        {
            if (m_Agent.remainingDistance <= m_Agent.stoppingDistance&&!isCrashing)
            {
                m_PatrolTimer += Time.deltaTime;
                if (m_PatrolTimer > patrolWaitTime)
                {
                    int n = Random.Range(0, m_AIRCToyCarData.m_RandomNumRange);
                    m_Agent.destination = m_AIRCToyCarData.WayPointPosition[n];
                    m_PatrolTimer = 0;
                }
            }
            if (isCrashing)
            {
                Vector3 movement = Vector3.forward * m_AIRCToyCarData.Speed * (-0.02f);
                m_Rigidbody.transform.position=Vector3.Lerp(m_Rigidbody.position,m_Rigidbody.transform.position + movement,m_AIRCToyCarData.Speed);
            }
            CarWheel();
        }
        //AI移动
        
        void CarWheel()
        {
            if (!isCrashing)
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
            if (isCrashing)
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
            if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player") ||
                 collision.gameObject.CompareTag("Wall")) && !isCrashing) 
            { 
                isCrashing = true; 
                Invoke("KnockBack", 0.8f); 
                GameEntry.Sound.PlaySound(30002); 
            }
            
            if (collision.gameObject.CompareTag("Player")||collision.gameObject.CompareTag("Enemy"))
            {            
                if (PlayerSkill.ShieldActive != 0)
                {
                    PlayerSkill.ShieldActive--;
                }
                else
                {
                    currentHP -= m_AIRCToyCarData.AttackDamage;
                }
            }
            //伤害计算1
            if (collision.gameObject.CompareTag("Missile"))
            {
                if (PlayerSkill.ShieldActive != 0)
                {
                    PlayerSkill.ShieldActive--;
                }
                else
                {
                    currentHP -= PlayerSkill.MissileAttackDamage;
                }
            }
            //伤害计算2
        }
        //AI碰撞玩家后被击退
        
        void KnockBack() 
        { 
            isCrashing = false; 
        }

    }
}