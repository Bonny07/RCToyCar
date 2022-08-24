using UnityEngine;
using GameFramework.Event;
using UnityEditor.Searcher;
using UnityEngine.SceneManagement;
using UnityGameFramework.Runtime;
using GameFramework.DataTable;

namespace RCToyCar
{
    public class MyRCToyCar : RCTOYCar
    {
        [SerializeField]
        private RCToyCarData m_MyRcToyCarData = null;
        
        float m_TurnSmoothVelocity=0.1f; //使小车转向时更顺滑
        public float carHorizontal;
        public float carVertical;
        private Vector3 m_Direction;
        public Transform cam;
        private float m_TargetAngle;
        public float turnSmoothTime = 0.1f; //玩家模型转向顺滑度
        private bool m_IsCrashing; //玩家是否处于被撞后的眩晕状态
        private Rigidbody m_Rigidbody;

        private Rect m_PlayerMoveBoundary = default(Rect);
        
        

        public float Shield;
        public float Speedup;
        public float Missile;
        public float ShieldActive;  //护盾是否正在生效
        public GameObject SkillShield;

        private int HealingHP;  //修理包回血量
        
        private float ShieldActiveTime;  //护盾抵挡伤害次数
        private float ShieldLastTime;  //护盾持续时间
        
        private float AddSpeedTimes;  //加速倍率
        private float AddSpeedLastTime;  //加速持续时间

        public float MissileAttackDamage;  //导弹击中造成伤害
        public float MissileFlySpeed;  //导弹飞行速度
        public float MissileLastTime;  //导弹持续时间
        
        public Rigidbody m_missile;
        public Transform m_FireTransform;

        private string m_FireButton;
        private bool m_Fired;

        
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            GameEntry.Event.Subscribe(JoyStickEventArgs.EventId,isMoving);
            GameEntry.Event.Subscribe(SkillEventArgs.EventId,UsingProp);
            cam = FindObjectOfType<Camera>().transform;
            m_Rigidbody = GetComponent<Rigidbody> ();
            
        }
        
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            PropOnLoad();
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
            Moving(); //移动
            skillpush();
            if (GameBase.GameResultNum != 0f)
            {
                SkillClear();
            }
            //结束游戏清空技能
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
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, m_TargetAngle, ref m_TurnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }

            if (!m_IsCrashing && (carHorizontal != 0||carVertical!=0)) 
            {
                CarWheel();
                Vector3 movement = Quaternion.Euler(0f, m_TargetAngle, 0f) * Vector3.forward * m_MyRcToyCarData.Speed * 0.015f;
                m_Rigidbody.transform.position = Vector3.Lerp(m_Rigidbody.position, m_Rigidbody.transform.position + movement, m_MyRcToyCarData.Speed);
            }
            if (m_IsCrashing)
            {
                CarWheel();
                Vector3 movement = Quaternion.Euler(0f, m_TargetAngle, 0f) * Vector3.forward * m_MyRcToyCarData.Speed * (-0.02f);
                m_Rigidbody.transform.position=Vector3.Lerp(m_Rigidbody.position,m_Rigidbody.transform.position + movement,m_MyRcToyCarData.Speed);
            }
        }
        //小车移动
        
        void CarWheel()
        {
            if (!m_IsCrashing && (carHorizontal != 0||carVertical!=0))
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
            if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player") ||
                 collision.gameObject.CompareTag("Wall")) && !m_IsCrashing) 
            { 
                m_IsCrashing = true; 
                Invoke("KnockBack", 0.8f); 
                GameEntry.Sound.PlaySound(30002); 
            }
            //玩家碰撞敌方后被击退
            
            if (collision.gameObject.CompareTag("Enemy")||collision.gameObject.CompareTag("Player"))
            {            
                if (PlayerSkill.ShieldActive != 0)
                {
                    PlayerSkill.ShieldActive--;
                }
                else
                {
                    m_MyRcToyCarData.HP -= m_MyRcToyCarData.AttackDamage;
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
                    m_MyRcToyCarData.HP -= 200;
                }
            }
            //伤害计算2
            if (collision.gameObject.CompareTag("HealthPack"))
            {
                GameEntry.Sound.PlaySound(30006);
                m_MyRcToyCarData.HP += HealingHP;
                Debug.Log("player拾取工具箱");
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
            if (Input.GetKey(KeyCode.Alpha2))
            {
                SpeedUpPush();
            }
            //使用加速道具
            if (Input.GetKey(KeyCode.Space))
            {
                MissilePush();
                MissileShoot();
            }
            //使用导弹道具
        }
        //玩家道具存储统计
        
        void SkillClear()
        {
            Missile = 0;
            Speedup = 0;
            Shield = 0;
            ShieldActive = 0;
        }
        //游戏结束，清空玩家技能
        
        void SpeedUp()
        {
            Movement.CarSpeed = Movement.StartCarSpeed;
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
        
        public void ShieldPush()
        {
            if (Shield == 1 && ShieldActive == 0)
            {
                ShieldActive = ShieldActiveTime;
                GameEntry.Sound.PlaySound(30003);
                Invoke("ShieldActivating", ShieldLastTime);
                Debug.Log("释放护盾");
                Shield--;
            }
        }
        
        public void SpeedUpPush()
        {
            if (Speedup == 1)
            {
                Invoke("SpeedUp", AddSpeedLastTime);
                GameEntry.Sound.PlaySound(30004);
                Movement.CarSpeed *= AddSpeedTimes;
                Debug.Log("释放加速");
                Speedup--;
            }
        }

        public void MissilePush()
        {
            if (Missile == 1)
            {
                GameEntry.Sound.PlaySound(30005);
                Debug.Log("释放导弹");
            }
        }
        
        public void MissileShoot()
        {
            if (Missile == 1)
            {
                Missile--;
                Fire();
            }
        }
        
        private void Fire()
        {
            Rigidbody shellInstance = Instantiate(m_missile, m_FireTransform.position, m_FireTransform.rotation);
            shellInstance.velocity = PlayerSkill.MissileFlySpeed * m_FireTransform.forward;
            Debug.Log("释放导弹");
        }
        //使用导弹道具后，从小车发射点向前发射一枚导弹
        
        
                
        private void isMoving(object sender, GameEventArgs e)
        {
            JoyStickEventArgs ne = (JoyStickEventArgs)e;
            carHorizontal = ne.Horizontal;
            carVertical = ne.Vertical;
        }
        //接受UI层虚拟摇杆信息
        
        private void UsingProp(object sender, GameEventArgs ee)
        {
            SkillEventArgs ne = (SkillEventArgs)ee;
            if (ne.Skillnum ==  0)
            {
                return;
            }

            if (ne.Skillnum == 1)
            {
                ShieldPush();
            }
            if (ne.Skillnum == 2)
            {
                SpeedUpPush();
            }
            if (ne.Skillnum == 3)
            {
                MissilePush();
                MissileShoot();
            }
        }
        
        void PropOnLoad()
        {
            IDataTable<DRProperties> dtProperties = GameEntry.DataTable.GetDataTable<DRProperties>();
            DRProperties drPropHealthPack = dtProperties.GetDataRow(20000);
            DRProperties drPropShield = dtProperties.GetDataRow(20001);
            DRProperties drPropSpeedup = dtProperties.GetDataRow(20002);
            DRProperties drPropMissile = dtProperties.GetDataRow(20003);
            if (drPropShield == null || drPropSpeedup == null || drPropHealthPack == null||drPropMissile==null)
            {
                return;
            }

            HealingHP = drPropHealthPack.Healing;
            
            ShieldActiveTime = drPropShield.PreventDamageTime;
            ShieldLastTime = drPropShield.LastTime;
            
            AddSpeedTimes = drPropSpeedup.AddSpeed;
            AddSpeedLastTime = drPropSpeedup.LastTime;
            
            MissileAttackDamage = drPropMissile.MissileDamage;
            MissileFlySpeed = drPropMissile.MissileSpeed;
            MissileLastTime = drPropMissile.LastTime;

        }
        //道具数据加载
    }
}