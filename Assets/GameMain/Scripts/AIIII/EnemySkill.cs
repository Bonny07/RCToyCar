using UnityEngine;
using GameFramework.DataTable;

namespace RCToyCar
{
    public class EnemySkill : MonoBehaviour
    {
        public static float Shield;
        public static float Speedup;
        public static float Missile;
        public static float ShieldActive1;  //护盾是否正在生效
        public GameObject SkillShield;
        public AudioClip ShieldActivate;
        public AudioClip SpeedupActivate;
        public AudioClip MissileActivate;

        private float EnemyRandomSkillPush;
        private float SkillPushRange;
        public static int skillnum;  //确保技能仅释放一次
        public static float EnemyShoot;  //敌方发射导弹

        private float ShieldActiveTime;  //护盾抵挡伤害次数
        private float ShieldLastTime;  //护盾持续时间
        
        private float AddSpeedTimes;  //加速倍率
        private float AddSpeedLastTime;  //加速持续时间


        private void Start()
        {
            PropOnLoad();
            CarDataOnLoad();
            skillnum = 0;
            EnemyShoot = 0f;
        }

        void Update()
        {
            if(skillnum == 1)
            {
                EnemyRandomSkillPush = Random.Range(1,SkillPushRange);
                Invoke("Enemyskillpush",EnemyRandomSkillPush);
                skillnum = 0;
            }
            
            //敌人在随机时间后释放技能
            if (GameManager.GameResultPlay != 0f)
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
        }

        public void Enemyskillpush()
        {
            if (Shield == 1 && ShieldActive1==0)
            {
                ShieldActive1 = ShieldActiveTime;
                Invoke("ShieldActivating", ShieldLastTime);
                AudioSource.PlayClipAtPoint(ShieldActivate, transform.position);
                Debug.Log("敌人释放护盾");
                Shield--;
                EnemyRandomSkillPush = Random.Range(1,SkillPushRange);
            }
            //使用护盾道具
            
            if (Speedup == 1)
            {
                Invoke("SpeedUp", AddSpeedLastTime);
                AudioSource.PlayClipAtPoint(SpeedupActivate, transform.position);
                Enemy.m_Agent.speed *= AddSpeedTimes;
                Debug.Log("敌人释放加速");
                Speedup--;
                EnemyRandomSkillPush = Random.Range(1,SkillPushRange);
            }
            //使用加速道具
            
            if (Missile == 1)
            {
                AudioSource.PlayClipAtPoint(MissileActivate, transform.position);
                EnemyShoot ++;
                Debug.Log("敌人释放导弹");
                Missile--;
                EnemyRandomSkillPush = Random.Range(1,SkillPushRange);
                
            }
            //使用导弹道具
        }
        //玩家道具存储统计
        
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
            DRProperties drPropShield = dtProperties.GetDataRow(20001);
            DRProperties drPropSpeedup = dtProperties.GetDataRow(20002);
            if (drPropShield == null || drPropSpeedup == null)
            {
                return;
            }

            ShieldActiveTime = drPropShield.PreventDamageTime;
            ShieldLastTime = drPropShield.LastTime;
            
            AddSpeedTimes = drPropSpeedup.AddSpeed;
            AddSpeedLastTime = drPropSpeedup.LastTime;
        }
        //道具数据加载

    }
}

