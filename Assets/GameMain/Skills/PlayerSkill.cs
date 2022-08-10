using UnityEngine;
using GameFramework.DataTable;

namespace RCToyCar
{
    public class PlayerSkill : MonoBehaviour
    {
        public static float Shield;
        public static float Speedup;
        public static float Missile;
        public static float ShieldActive;  //护盾是否正在生效
        public GameObject SkillShield;
        public AudioClip ShieldActivate;
        public AudioClip SpeedupActivate;
        public AudioClip MissileActivate;

        public static float HealingHP;  //修理包回血量
        
        private float ShieldActiveTime;  //护盾抵挡伤害次数
        private float ShieldLastTime;  //护盾持续时间
        
        private float AddSpeedTimes;  //加速倍率
        private float AddSpeedLastTime;  //加速持续时间

        public static float MissileAttackDamage;  //导弹击中造成伤害
        public static float MissileFlySpeed;  //导弹飞行速度
        public static float MissileLastTime;  //导弹持续时间

        private void Start()
        {
            PropOnLoad();
        }

        void Update()
        {
            skillpush();
            if (GameManager.GameResultPlay != 0f)
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
            }
            //使用导弹道具
        }
        //玩家道具存储统计
        
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
        
        void SkillClear()
        {
            Missile = 0;
            Speedup = 0;
            Shield = 0;
            ShieldActive = 0;
        }
        //游戏结束，清空玩家技能

        public void ShieldPush()
        {
            if (Shield == 1 && ShieldActive == 0)
            {
                ShieldActive = ShieldActiveTime;
                Invoke("ShieldActivating", ShieldLastTime);
                AudioSource.PlayClipAtPoint(ShieldActivate, transform.position);
                Debug.Log("释放护盾");
                Shield--;
            }
        }

        public void SpeedUpPush()
        {
            if (Speedup == 1)
            {
                Invoke("SpeedUp", AddSpeedLastTime);
                AudioSource.PlayClipAtPoint(SpeedupActivate, transform.position);
                Movement.CarSpeed *= AddSpeedTimes;
                Debug.Log("释放加速");
                Speedup--;
            }
        }

        public void MissilePush()
        {
            if (Missile == 1)
            {
                AudioSource.PlayClipAtPoint(MissileActivate, transform.position);
                Debug.Log("释放导弹");
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

