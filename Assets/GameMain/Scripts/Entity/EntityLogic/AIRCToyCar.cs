using UnityEngine;
using GameFramework.Event;
using UnityEditor.Searcher;
using UnityEngine.SceneManagement;
using UnityGameFramework.Runtime;
using UnityEngine.AI;
using GameFramework.DataTable;

namespace RCToyCar
{
    public class AIRCToyCar : RCTOYCar
    {
        private AIRCToyCarData m_AIRcToyCarData = null;
        public float patrolWaitTime = 0f;
        public static NavMeshAgent m_Agent;  //存储游戏进行时敌人小车速度

        private Rigidbody m_Rigidbody;
        private float m_PatrolTimer;
        private int RandomNumRange;
        
        private bool isCrashing=false; //是否处于被撞后的眩晕状态
        private Vector3 WayPointPosition;

        
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_Rigidbody = GetComponent<Rigidbody> ();
             /*= m_AIRcToyCarData.Speed;*/
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_AIRcToyCarData = userData as AIRCToyCarData;
            if (m_AIRcToyCarData == null)
            {
                Log.Error("AI RCToyCar data is invalid.");
                return;
            }
        }
        

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            Patrolling(); //移动
        }

        void Patrolling()
        {
            if ((m_Agent.remainingDistance <= m_Agent.stoppingDistance)&&!isCrashing)
            {
                m_PatrolTimer += Time.deltaTime;
                if (m_PatrolTimer > patrolWaitTime)
                {
                    m_Agent.destination = WayPointPosition;
                    m_PatrolTimer = 0;
                }
            }
            if (isCrashing)
            {
                Vector3 movement = Vector3.forward * m_AIRcToyCarData.Speed * (-0.02f);
                m_Rigidbody.transform.position=Vector3.Lerp(m_Rigidbody.position,m_Rigidbody.transform.position + movement,m_AIRcToyCarData.Speed);
            }
            CarWheel();
        }
        
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
        }
        //AI碰撞玩家后被击退
        
        void KnockBack() 
        { 
            isCrashing = false; 
        }
    }
}