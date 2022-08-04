using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RCToyCar
{
    public class Actor : MonoBehaviour
    {
        public Transform goal;

        private NavMeshAgent m_Agent;

        // Start is called before the first frame update
        void Start()
        {
            m_Agent = GetComponent<NavMeshAgent>();
            m_Agent.destination = goal.position;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}