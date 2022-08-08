using System;
using UnityEngine;
using UnityEngine.AI;
using GameFramework.DataTable;

namespace RCToyCar
{
    public class Enemy : MonoBehaviour
    {
        public float patrolWaitTime = 0.5f;
        public Transform patrolWayPoints;
        public static NavMeshAgent m_Agent;
        
        private float m_PatrolTimer;
        private int m_WayPointIndex;
        private int pointnum;
        private float CarSpeed;


        void Start()
        {
            SpeedOnLoad();
            m_Agent = GetComponent<NavMeshAgent>();
            m_Agent.destination = patrolWayPoints.GetChild(m_WayPointIndex).position;
            m_Agent.isStopped = false;
            m_Agent.speed = CarSpeed;
            pointnum = patrolWayPoints.childCount;
        }

        void Update()
        {
            Patrolling();
        }

        /// <summary>
        /// 
        /// </summary>
        void Patrolling()
        {
            if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
            {
                m_PatrolTimer += Time.deltaTime;
                if (m_PatrolTimer > patrolWaitTime)
                {
                    m_WayPointIndex = GetRandomIndex(m_WayPointIndex, pointnum);
                    m_Agent.destination = patrolWayPoints.GetChild(m_WayPointIndex).position;
                    m_PatrolTimer = 0;
                }
            }
        }

        /// <summary>
        ///  从 [0, range-1] 中随机一个不等于num的数 
        /// </summary>
        /// <param name="num"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        private static int GetRandomIndex(int num, int range)
        {
            int r = UnityEngine.Random.Range(0, range);
            while (r == num)
            {
                r = UnityEngine.Random.Range(0, range);
            }

            return r;
        }
        
        void SpeedOnLoad()
        {
            IDataTable<DRRCToyCar> dtSpeed = GameEntry.DataTable.GetDataTable<DRRCToyCar>();
            DRRCToyCar drCarSpeed = dtSpeed.GetDataRow(10001);
            if (drCarSpeed == null)
            {
                return;
            }
            CarSpeed = drCarSpeed.Speed;
        }
    }
}