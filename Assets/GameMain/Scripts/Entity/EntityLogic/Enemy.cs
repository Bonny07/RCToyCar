using System;
using UnityEngine;
using UnityEngine.AI;
using GameFramework.DataTable;

namespace RCToyCar
{
    public class Enemy : MonoBehaviour
    {
        public float patrolWaitTime = 0.5f;
        public static NavMeshAgent m_Agent;  //存储游戏进行时敌人小车速度
        public static float StartCarSpeed;  //存储敌人小车常规速度

        private Rigidbody m_Rigidbody;
        private float m_PatrolTimer;
        private int RandomNumRange;
        
        private bool isCrashing1=false; //是否处于被撞后的眩晕状态
        private Vector3 WayPointPosition;



        void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            SpeedOnLoad();
            WayPointRangeOnLoad();
            WayPointOnLoad();
            m_Agent = GetComponent<NavMeshAgent>();
            m_Agent.destination = WayPointPosition;
            m_Agent.isStopped = false;
            m_Agent.speed = StartCarSpeed;
        }

        void FixedUpdate()
        {
            ECarWheel();
            if (isCrashing1)
            {
                Vector3 movement = Vector3.right * StartCarSpeed * (-0.04f);
                m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
            }

            Patrolling();
            
        }

        /// <summary>
        /// 
        /// </summary>
        void Patrolling()
        {
            if ((m_Agent.remainingDistance <= m_Agent.stoppingDistance)&&!isCrashing1)
            {
                m_PatrolTimer += Time.deltaTime;
                if (m_PatrolTimer > patrolWaitTime)
                {
                    WayPointOnLoad();
                    m_Agent.destination = WayPointPosition;
                    m_PatrolTimer = 0;
                }
            }
        }
        
        
        void SpeedOnLoad()
        {
            IDataTable<DRRCToyCar> dtSpeed = GameEntry.DataTable.GetDataTable<DRRCToyCar>();
            DRRCToyCar drCarSpeed = dtSpeed.GetDataRow(10001);
            if (drCarSpeed == null)
            {
                return;
            }
            StartCarSpeed = drCarSpeed.Speed;
        }
        //读表获取小车速度
        
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player")&&!isCrashing1)
            {
                isCrashing1 = true;
                GameEntry.Sound.PlaySound(30002);
                Invoke("KnockBack1", 0.8f);
            }
            //玩家碰撞敌方后被击退
        }
        void KnockBack1()
        {
            isCrashing1 = false;
        }

        void ECarWheel()
        {
            GameObject mygameobject1 = GameObject.Find("Emodel_LeftF");
            GameObject mygameobject2 = GameObject.Find("Emodel_LeftB");
            GameObject mygameobject3 = GameObject.Find("Emodel_RightF");
            GameObject mygameobject4 = GameObject.Find("Emodel_RightB");
            mygameobject1.transform.Rotate(Vector3.forward, 1 * Time.time);
            mygameobject2.transform.Rotate(Vector3.forward, 1 * Time.time);
            mygameobject3.transform.Rotate(Vector3.forward, 1 * Time.time);
            mygameobject4.transform.Rotate(Vector3.forward, 1 * Time.time);
        }
        //小车运动时使小车轮子滚动

        void WayPointRangeOnLoad()
        {
            IDataTable<DRWayPoint> dtPositionNum = GameEntry.DataTable.GetDataTable<DRWayPoint>();
            DRWayPoint drPositionNum = dtPositionNum.GetDataRow(10000);
            if (drPositionNum == null)
            {
                return;
            }
            RandomNumRange = drPositionNum.WayPointNum;
        }
        //读取路径点数量，供随机函数使用

        void WayPointOnLoad()
        {
            IDataTable<DRWayPoint> dtPosition = GameEntry.DataTable.GetDataTable<DRWayPoint>();
            DRWayPoint drPosition = dtPosition.GetDataRow(GetRandomIndex(RandomNumRange));
            if (drPosition == null)
            {
                return;
            }
            WayPointPosition = drPosition.Position;
        }
        //读取路径点位置信息
        
        private static int GetRandomIndex(int range)
        {
           int r = UnityEngine.Random.Range(0, range);
           return r;
        }
        //  从 [0, range-1] 中随机一个不等于num的数 
    }
}