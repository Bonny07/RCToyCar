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
        private static NavMeshAgent s_Agent;  //存储游戏进行时敌人小车速度
        private Rigidbody m_Rigidbody;
        private float m_PatrolTimer;
        private int m_RandomNumRange;

        private bool isCrashing=false; //是否处于被撞后的眩晕状态
        private Vector3 WayPointPosition;
        
        private int HealingHP;  //修理包回血量
        
        public static float Shield;
        public static float Speedup;
        public static float Missile;
        public static float ShieldActive1;  //护盾是否正在生效
        public GameObject SkillShield;

        private float EnemyRandomSkillPush;
        private float SkillPushRange;
        public static int skillnum;  //确保技能仅释放一次
        public static float EnemyShoot;  //敌方发射导弹

        private float ShieldActiveTime;  //护盾抵挡伤害次数
        private float ShieldLastTime;  //护盾持续时间
        
        private float AddSpeedTimes;  //加速倍率
        private float AddSpeedLastTime;  //加速持续时间
        
        public Rigidbody m_missile;
        public Transform m_FireTransform;

        private string m_FireButton;
        private bool m_Fired;

        
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_Rigidbody = GetComponent<Rigidbody> ();
            s_Agent = GetComponent<NavMeshAgent>();
            s_Agent.isStopped = false;

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
            s_Agent.speed= m_AIRCToyCarData.Speed;
            s_Agent.destination = m_AIRCToyCarData.WayPointPosition[1];
        }
        

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            Patrolling(); //移动
            Skill();
        }

        void Patrolling()
        {
            if (s_Agent.remainingDistance <= s_Agent.stoppingDistance&&!isCrashing)
            {
                m_PatrolTimer += Time.deltaTime;
                if (m_PatrolTimer > patrolWaitTime)
                {
                    int n = Random.Range(0, m_AIRCToyCarData.m_RandomNumRange-1);
                    s_Agent.destination = m_AIRCToyCarData.WayPointPosition[n];
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
                    m_AIRCToyCarData.HP -= m_AIRCToyCarData.AttackDamage;
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
                    m_AIRCToyCarData.HP -= 200;
                }
            }
            //伤害计算2
            if (collision.gameObject.CompareTag("HealthPack"))
            {
                GameEntry.Sound.PlaySound(30006);
                m_AIRCToyCarData.HP += HealingHP;
                Debug.Log("AI拾取工具箱");
            }
        }
        //AI碰撞玩家后被击退
        
        void KnockBack() 
        { 
            isCrashing = false; 
        }

        void Skill()
        {
            if(skillnum == 1)
            {
                EnemyRandomSkillPush = Random.Range(1,SkillPushRange);
                Invoke("Enemyskillpush",EnemyRandomSkillPush);
                skillnum = 0;
            }
            
            //敌人在随机时间后释放技能
            if (GameBase.GameResultNum != 0f)
            {
                SkillClear();
            }
            //结束游戏清空技能

            if (ShieldActive1 != 0f)
            {
                SkillShield.SetActive(true);
            }
            else
            {
                SkillShield.SetActive(false);
            }
            //控制护盾视觉效果
            
            if (EnemyShoot == 1)
            {
                EnemyShoot--;
                Fire();
                Debug.Log("敌人释放导弹");
            }
        }
        
        public void Enemyskillpush()
        {
            if (Shield == 1 && ShieldActive1==0)
            {
                ShieldActive1 = ShieldActiveTime;
                Invoke("ShieldActivating", ShieldLastTime);
                GameEntry.Sound.PlaySound(30003);
                Debug.Log("敌人释放护盾");
                Shield--;
                EnemyRandomSkillPush = Random.Range(1,SkillPushRange);
            }
            //使用护盾道具
            
            if (Speedup == 1)
            {
                Invoke("SpeedUp", AddSpeedLastTime);
                GameEntry.Sound.PlaySound(30004);
                Enemy.m_Agent.speed *= AddSpeedTimes;
                Debug.Log("敌人释放加速");
                Speedup--;
                EnemyRandomSkillPush = Random.Range(1,SkillPushRange);
            }
            //使用加速道具
            
            if (Missile == 1)
            {
                GameEntry.Sound.PlaySound(30005);
                EnemyShoot ++;
                Debug.Log("敌人释放导弹");
                Missile--;
                EnemyRandomSkillPush = Random.Range(1,SkillPushRange);
                
            }
            //使用导弹道具
        }
        //AI道具存储统计
        
        void SpeedUp()
        {
            Enemy.m_Agent.speed = Enemy.StartCarSpeed;
        }
        //加速超出持续时间

        void ShieldActivating()
        {
            if (ShieldActive1 != 0)
            {
                ShieldActive1 = 0;
            }
        }
        //护盾超出持续时间
        
        private void Fire()
        {
            Rigidbody shellInstance = Instantiate(m_missile, m_FireTransform.position, m_FireTransform.rotation);
            shellInstance.velocity = PlayerSkill.MissileFlySpeed * m_FireTransform.forward;
        }
        //使用导弹道具后，从敌人小车发射点向前发射一枚导弹
        
        void SkillClear()
        {
            Missile = 0;
            Speedup = 0;
            Shield = 0;
            ShieldActive1 = 0;
            skillnum = 0;
        }
        //游戏结束，清空玩家技能
        
        void CarDataOnLoad()
        {
            IDataTable<DRRCToyCar> dtCarData = GameEntry.DataTable.GetDataTable<DRRCToyCar>();
            DRRCToyCar drCarData = dtCarData.GetDataRow(10001);
            if (drCarData == null)
            {
                return;
            }
            SkillPushRange = drCarData.SkillRange;
        }

        void PropOnLoad()
        {
            IDataTable<DRProperties> dtProperties = GameEntry.DataTable.GetDataTable<DRProperties>();
            DRProperties drPropHealthPack = dtProperties.GetDataRow(20000);
            DRProperties drPropShield = dtProperties.GetDataRow(20001);
            DRProperties drPropSpeedup = dtProperties.GetDataRow(20002);
            if (drPropShield == null || drPropSpeedup == null)
            {
                return;
            }
            HealingHP = drPropHealthPack.Healing;

            ShieldActiveTime = drPropShield.PreventDamageTime;
            ShieldLastTime = drPropShield.LastTime;
            
            AddSpeedTimes = drPropSpeedup.AddSpeed;
            AddSpeedLastTime = drPropSpeedup.LastTime;
        }
        //道具数据加载

    }
}