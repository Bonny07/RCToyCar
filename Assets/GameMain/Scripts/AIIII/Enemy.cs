using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RCToyCar
{
    public class Enemy : MonoBehaviour
    {
        public float patrolSpeed = 8f;
        public float patrolWaitTime = 0.5f;
        public Transform patrolWayPoints;
        private int pointnum;

        public static NavMeshAgent m_Agent;
        private float m_PatrolTimer = 0;
        private int m_WayPointIndex = 0;


        void Start()
        {
            m_Agent = GetComponent<NavMeshAgent>();
            m_Agent.destination = patrolWayPoints.GetChild(m_WayPointIndex).position;
            m_Agent.isStopped = false;
            m_Agent.speed = patrolSpeed;
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
    }
}