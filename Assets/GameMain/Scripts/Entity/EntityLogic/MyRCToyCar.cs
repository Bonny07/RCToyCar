using UnityEngine;
using GameFramework.Event;
using UnityGameFramework.Runtime;

namespace RCToyCar
{
    public class MyRCToyCar : RCTOYCar
    {
        float m_TurnSmoothVelocity = 0.1f; //使小车转向时更顺滑
        public float carHorizontal;
        public float carVertical;
        private Vector3 m_Direction;
        public Transform cam;
        private float m_TargetAngle;
        public float turnSmoothTime = 0.1f; //玩家模型转向顺滑度
        private bool m_IsCrashing; //玩家是否处于被撞后的眩晕状态
        private Rigidbody m_Rigidbody;
        private float CurrentSpeed;

        private Rect m_PlayerMoveBoundary = default(Rect);

        private MyRCToyCarData m_MyRcToyCarData;

        public float Shield; //持有护盾数量
        public float Speedup; //持有加速数量
        public float Missile; //持有导弹数量
        public float ShieldActive; //护盾是否正在生效
        public GameObject SkillShield;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            GameEntry.Event.Subscribe(JoyStickEventArgs.EventId, isMoving);
            GameEntry.Event.Subscribe(SkillEventArgs.EventId, UsingProp);
            cam = FindObjectOfType<Camera>().transform;
            m_Rigidbody = GetComponent<Rigidbody>();
        }
        //初始化小车组件

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_MyRcToyCarData = userData as MyRCToyCarData;
            SkillShield = transform.Find("Shield").gameObject;
            if (m_MyRcToyCarData == null)
            {
                Log.Error("My RCToyCar data is invalid.");
                return;
            }
            Shield = 0;
            Speedup = 0;
            Missile = 0;
            GameEntry.Event.Fire(this,new SkillStorageEventArgs(){ShieldStorage = 2});
            GameEntry.Event.Fire(this,new SkillStorageEventArgs(){SpeedUpStorage = 2});
            GameEntry.Event.Fire(this,new SkillStorageEventArgs(){MissileStorage = 2});
            CurrentSpeed = m_MyRcToyCarData.Speed;

        }
        //重置小车组件


        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            Moving(); //移动
            skillpush();
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


        void Moving()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                carHorizontal = Input.GetAxisRaw("Horizontal");
                carVertical = Input.GetAxisRaw("Vertical");
            }

            m_Direction = new Vector3(carHorizontal, 0f, carVertical).normalized;
            if (m_Direction.magnitude >= 0.1f)
            {
                m_TargetAngle = Mathf.Atan2(m_Direction.x, m_Direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, m_TargetAngle, ref m_TurnSmoothVelocity,
                    turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }

            if (!m_IsCrashing && (carHorizontal != 0 || carVertical != 0))
            {
                CarWheel();
                Vector3 movement = Quaternion.Euler(0f, m_TargetAngle, 0f) * Vector3.forward * CurrentSpeed * 0.015f;
                m_Rigidbody.transform.position = Vector3.Lerp(m_Rigidbody.position,
                    m_Rigidbody.transform.position + movement, m_MyRcToyCarData.Speed);
            }

            if (m_IsCrashing)
            {
                CarWheel();
                Vector3 movement = Quaternion.Euler(0f, m_TargetAngle, 0f) * Vector3.forward * CurrentSpeed * (-0.02f);
                m_Rigidbody.transform.position = Vector3.Lerp(m_Rigidbody.position,
                    m_Rigidbody.transform.position + movement, m_MyRcToyCarData.Speed);
            }
        }
        //小车移动

        void CarWheel()
        {
            if (!m_IsCrashing && (carHorizontal != 0 || carVertical != 0))
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
            //玩家碰撞敌方后被击退

            else if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player")) &&
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
                    m_MyRcToyCarData.HP -= m_MyRcToyCarData.AttackDamage;
                }
            }

            //伤害计算1
            else if (collision.gameObject.CompareTag("Missile"))
            {
                if (ShieldActive != 0)
                {
                    ShieldActive--;
                }
                else
                {
                    m_MyRcToyCarData.HP -= m_MyRcToyCarData.MisslieDamage;
                }
            }
            //伤害计算2
        }

        void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("HealthPack"))
            {
                GameEntry.Sound.PlaySound(30006);
                m_MyRcToyCarData.HP += m_MyRcToyCarData.Healing;
                Debug.Log("player拾取工具箱");
            }
            //治疗包回复生命

            else if (collision.gameObject.CompareTag("Shield"))
            {
                GameEntry.Sound.PlaySound(30000);
                Shield=1;
                GameEntry.Event.Fire(this,new SkillStorageEventArgs(){ShieldStorage = 1});
                Debug.Log("player拾取护盾");
            }

            else if (collision.gameObject.CompareTag("SpeedUp"))
            {
                GameEntry.Sound.PlaySound(30000);
                Speedup=1;
                GameEntry.Event.Fire(this,new SkillStorageEventArgs(){SpeedUpStorage= 1});
                Debug.Log("player拾取加速");
            }

            else if (collision.gameObject.CompareTag("MissileProp"))
            {
                GameEntry.Sound.PlaySound(30000);
                Missile=1;
                GameEntry.Event.Fire(this,new SkillStorageEventArgs(){MissileStorage= 1});
                Debug.Log("player拾取导弹");
            }
        }

        void KnockBack()
        {
            m_IsCrashing = false;
        }

        public void skillpush()
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                ShieldPush();
            }

            //使用护盾道具
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                SpeedUpPush();
            }

            //使用加速道具
            else if (Input.GetKey(KeyCode.Space))
            {
                MissilePush();
            }
            //使用导弹道具*/
        }

        //键盘释放道具
        private void UsingProp(object sender, GameEventArgs e)
        {
            SkillEventArgs ne = (SkillEventArgs)e;
            if (ne.Skillnum == 0)
            {
                return;
            }

            if (ne.Skillnum == 1)
            {
                ShieldPush();
            }

            else if (ne.Skillnum == 2)
            {
                SpeedUpPush();
            }

            else if (ne.Skillnum == 3)
            {
                MissilePush();
            }
        }
        //UI按钮释放道具

        public void ShieldPush()
        {
            if (Shield == 1 && ShieldActive == 0)
            {
                ShieldActive = m_MyRcToyCarData.PreventDamageTime;
                GameEntry.Sound.PlaySound(30003);
                Invoke("ShieldActivating", m_MyRcToyCarData.ShieldLastTime);
                Debug.Log("释放护盾");
                Shield=0;
                GameEntry.Event.Fire(this,new SkillStorageEventArgs(){ShieldStorage = 2});
            }
        }

        public void SpeedUpPush()
        {
            if (Speedup == 1)
            {
                Invoke("SpeedUp", m_MyRcToyCarData.AddSpeedLastTime);
                GameEntry.Sound.PlaySound(30004);
                CurrentSpeed *= m_MyRcToyCarData.AddSpeed;
                Debug.Log("player释放加速");
                Speedup=0;
                GameEntry.Event.Fire(this,new SkillStorageEventArgs(){SpeedUpStorage = 2});
            }
        }

        void SpeedUp()
        {
            CurrentSpeed = m_MyRcToyCarData.Speed;
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

        public void MissilePush()
        {
            if (Missile == 1)
            {
                m_Properties.MissilePush();
                GameEntry.Sound.PlaySound(30005);
                Debug.Log("player释放加速");
                Missile=0;
                GameEntry.Event.Fire(this,new SkillStorageEventArgs(){MissileStorage = 2});
            }
        }

        private void isMoving(object sender, GameEventArgs e)
        {
            JoyStickEventArgs ne = (JoyStickEventArgs)e;
            carHorizontal = ne.Horizontal;
            carVertical = ne.Vertical;
        }
        //接受UI层虚拟摇杆信息
    }
}