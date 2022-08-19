using System;
using UnityEngine;
using GameFramework.DataTable;
using UnityEngine.TextCore.Text;

namespace RCToyCar
{
    public class Movement : MonoBehaviour
    {
        Vector3 m_Velocity;
        float m_TurnSmoothVelocity; //使小车转向时更顺滑
        private Transform GameCanvas;
        public Joystick joystick;
        private float horizontal;
        private float vertical;

        public Transform cam;
        public float turnSmoothTime = 0.1f; //玩家模型转向顺滑度

        private Rigidbody m_Rigidbody;
        private bool isCrashing; //玩家是否处于被撞后的眩晕状态
        private float targetAngle;
        public static float CarSpeed; //存储游戏进行时玩家小车速度
        public static float StartCarSpeed; //存储玩家小车常规速度
        private Vector3 direction;

        private void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            SpeedOnLoad();
            CarSpeed = StartCarSpeed;
            joystick = FindObjectOfType<Joystick>();
        }


        private void Update()
        {
            /*horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");*/
            //键盘操作
            horizontal = joystick.Horizontal;
            vertical = joystick.Vertical;
            //虚拟摇杆操作
            direction = new Vector3(horizontal, 0f, vertical).normalized;

            CarWheel();

            if (direction.magnitude >= 0.1f)
            {
                targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_TurnSmoothVelocity,
                    turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
        //玩家人物移动，镜头控制人物移动方向

        void FixedUpdate()
        {
            /*if (Input.GetKey(KeyCode.W) && isCrashing == false || Input.GetKey(KeyCode.A) && isCrashing == false ||
                Input.GetKey(KeyCode.S) && isCrashing == false || Input.GetKey(KeyCode.D) && isCrashing == false)
            {
                Vector3 movement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * CarSpeed * 0.01f;
                m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
            }*/
            //键盘操作
            if ((horizontal != 0 || vertical != 0) && !isCrashing)
            {
                Vector3 movement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * CarSpeed * (0.02f);
                m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
            }
            //玩家移动

            if (isCrashing)
            {
                Vector3 movement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * CarSpeed * (-0.02f);
                m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
                
            }
            //玩家碰撞敌方后被击退
        }

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

        void SpeedOnLoad()
        {
            IDataTable<DRRCToyCar> dtSpeed = GameEntry.DataTable.GetDataTable<DRRCToyCar>();
            DRRCToyCar drCarSpeed = dtSpeed.GetDataRow(10000);
            if (drCarSpeed == null)
            {
                return;
            }

            StartCarSpeed = drCarSpeed.Speed;
        }
        //读表获取小车速度

        void CarWheel()
        {
            if (horizontal != 0 || vertical != 0)
            {
                GameObject mygameobject1 = GameObject.Find("model_LeftF");
                GameObject mygameobject2 = GameObject.Find("model_LeftB");
                GameObject mygameobject3 = GameObject.Find("model_RightF");
                GameObject mygameobject4 = GameObject.Find("model_RightB");
                mygameobject1.transform.Rotate(Vector3.forward, 1 * Time.time);
                mygameobject2.transform.Rotate(Vector3.forward, 1 * Time.time);
                mygameobject3.transform.Rotate(Vector3.forward, 1 * Time.time);
                mygameobject4.transform.Rotate(Vector3.forward, 1 * Time.time);
            }
        }
    }
}