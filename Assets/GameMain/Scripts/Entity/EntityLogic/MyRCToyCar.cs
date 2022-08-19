using UnityEngine;
using GameFramework.Event;
using UnityEditor.Searcher;
using UnityEngine.SceneManagement;
using UnityGameFramework.Runtime;

namespace RCToyCar
{
    public class MyRCToyCar : RCTOYCar
    {
        [SerializeField]
        private RCToyCarData m_MyRcToyCarData = null;
        float m_TurnSmoothVelocity=0.1f; //使小车转向时更顺滑
        public float carHorizontal;
        public float carVertical;
        private Vector3 direction;
        public Transform cam;
        private float targetAngle;
        public float turnSmoothTime = 0.1f; //玩家模型转向顺滑度
        private bool isCrashing; //玩家是否处于被撞后的眩晕状态
        private Rigidbody m_Rigidbody;
        
        private Rect m_PlayerMoveBoundary = default(Rect);

        
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            GameEntry.Event.Subscribe(JoyStickEventArgs.EventId,isMoving);
            cam = FindObjectOfType<Camera>().transform;
            m_Rigidbody = GetComponent<Rigidbody> ();
        }
        
        private void isMoving(object sender, GameEventArgs e)
        {
            JoyStickEventArgs ne = (JoyStickEventArgs)e;
            carHorizontal = ne.Horizontal;
            carVertical = ne.Vertical;
        }
        //接受UI层虚拟摇杆信息
        
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_MyRcToyCarData = userData as MyRCToyCarData;
            if (m_MyRcToyCarData == null)
            {
                Log.Error("My RCToyCar data is invalid.");
                return;
            }
        }
        

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                carHorizontal = Input.GetAxisRaw("Horizontal");
                carVertical = Input.GetAxisRaw("Vertical");
            }
            direction = new Vector3(carHorizontal, 0f, carVertical).normalized;
            if (direction.magnitude >= 0.1f)
            {
                targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_TurnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }

            if (!isCrashing && (carHorizontal != 0||carVertical!=0)) 
            {
                CarWheel();
                Vector3 movement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * m_MyRcToyCarData.Speed * 0.015f;
                m_Rigidbody.transform.position = Vector3.Lerp(m_Rigidbody.position, m_Rigidbody.transform.position + movement, m_MyRcToyCarData.Speed);
            }
            if (isCrashing)
            {
                CarWheel();
                Vector3 movement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * m_MyRcToyCarData.Speed * (-0.02f);
                m_Rigidbody.transform.position=Vector3.Lerp(m_Rigidbody.position,m_Rigidbody.transform.position + movement,m_MyRcToyCarData.Speed);
            }
        }
            //移动
            
            void CarWheel()
            {
                if (!isCrashing && (carHorizontal != 0||carVertical!=0))
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
            //玩家碰撞敌方后被击退
        }
        
        void KnockBack() 
        { 
            isCrashing = false; 
        }

        
    }
}