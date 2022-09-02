using GameFramework.DataTable;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RCToyCar
{
    public abstract class RCToyCarData : TargetableObjectData
    {
        [SerializeField] private int m_MaxHP = 0;
        //初始血量

        [SerializeField] private float m_Speed = 0;
        //初始速度

        [SerializeField] private int m_AttackDamage = 0;
        //初始攻击力

        [SerializeField] private int m_PropStorage = 0;
        //初始道具存储上限数量

        [SerializeField] private float m_SkillRange = 0;
        //敌人道具释放时间

        [SerializeField] private int m_HitSound = 0;
        //撞击音效

        [SerializeField] private int m_DeadEffectId = 0;
        //死亡音效
        
        [SerializeField] private int m_Healing = 0;
        //回复血量
        
        [SerializeField] private float m_PreventDamageTime = 0;
        //抵挡伤害次数
        
        [SerializeField] private float m_AddSpeed = 0;
        //移速倍率
        
        [SerializeField] private int m_MisslieDamage = 0;
        //导弹伤害
        
        [SerializeField] private float m_ShieldLastTime = 0;
        //护盾持续时间
        
        [SerializeField] private float m_AddSpeedLastTime = 0;
        //护盾持续时间

        [SerializeField] private float m_SkillPushRange = 0;

        [SerializeField] private PropertiesData m_PropertiesData = null;
        

        public RCToyCarData(int entityId, int typeId, CampType camp)
            : base(entityId, typeId, camp)
        {
            IDataTable<DRRCToyCar> dtRCToyCar = GameEntry.DataTable.GetDataTable<DRRCToyCar>();
            DRRCToyCar drRCToyCar = dtRCToyCar.GetDataRow(TypeId);
            if (drRCToyCar == null)
            {
                return;
            }
            HP = drRCToyCar.MaxHP;
            m_Speed = drRCToyCar.Speed;
            m_AttackDamage = drRCToyCar.CarAttackDamage;
            m_PropStorage = drRCToyCar.PropStorage;
            m_SkillRange = drRCToyCar.SkillRange;


            m_PropertiesData = new PropertiesData(GameEntry.Entity.GenerateSerialId(), drRCToyCar.PropId, Id, Camp);
            
            IDataTable<DRProperties> dtProperties = GameEntry.DataTable.GetDataTable<DRProperties>();
            DRProperties drProperties = dtProperties.GetDataRow(TypeId);
            if (drProperties == null)
            {
                return;
            }
            m_Healing = drProperties.Healing;
            m_PreventDamageTime = drProperties.PreventDamageTime;
            m_ShieldLastTime = drProperties.ShieldLastTime;
            m_AddSpeed = drProperties.AddSpeed;
            m_AddSpeedLastTime = drProperties.SpeedUpLastTime;
            m_MisslieDamage = drProperties.MissileDamage;

        }

        //读取小车数据
        public override int MaxHP
        {
            get { return m_MaxHP; }
        }

        public float Speed
        {
            get { return m_Speed; }
        }

        public int AttackDamage
        {
            get { return m_AttackDamage; }
        }

        public float PropStorage
        {
            get { return m_PropStorage; }
        }

        public float SkillRange
        {
            get { return m_SkillRange; }
        }

        public float HitSound
        {
            get { return m_HitSound; }
        }

        public int DeadEffectId
        {
            get { return m_DeadEffectId; }
        }

        public int Healing
        {
            get { return m_Healing; }
        }

        public float PreventDamageTime
        {
            get { return m_PreventDamageTime; }
        }

        public float AddSpeed
        {
            get { return m_AddSpeed; }
        }

        public int MisslieDamage
        {
            get { return m_MisslieDamage; }
        }

        public float ShieldLastTime
        {
            get { return m_ShieldLastTime; }
        }

        public float AddSpeedLastTime
        {
            get { return m_AddSpeedLastTime; }
        }

        public float SkillPushRange
        {
            get { return m_SkillPushRange; }
        }
        
        public PropertiesData GetPropertiesData()
        {
            return m_PropertiesData;
        }
    }
}