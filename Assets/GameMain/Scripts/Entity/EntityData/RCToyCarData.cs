using GameFramework.DataTable;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RCToyCar
{
    [Serializable]
    public abstract class RCToyCarData : TargetableObjectData
    {
        
        [SerializeField]
        private float m_HP = 0;  
        //初始血量
        
        [SerializeField]
        private float m_Speed = 0;
        //初始速度
        
        [SerializeField]
        private float m_AttackDamage = 0;
        //初始攻击力
        
        [SerializeField]
        private int m_PropStorage = 0;
        //初始道具存储上限数量

        [SerializeField]
        private float m_SkillRange = 0;
        //敌人道具释放时间
                
        [SerializeField]
        private int m_HitSound = 0;
        //撞击音效
        
        [SerializeField]
        private int m_DeadEffectId = 0;
        //死亡音效
        
        public RCToyCarData(int entityId, int typeId, CampType camp)
            : base(entityId, typeId, camp)
        {
            IDataTable<DRRCToyCar> dtRCToyCar = GameEntry.DataTable.GetDataTable<DRRCToyCar>();
            DRRCToyCar drRCToyCar = dtRCToyCar.GetDataRow(TypeId);
            if (drRCToyCar == null)
            {
                return;
            }
            m_HP = drRCToyCar.MaxHP;
            m_Speed = drRCToyCar.Speed;
            m_AttackDamage = drRCToyCar.CarAttackDamage;
            m_PropStorage = drRCToyCar.PropStorage;
            m_SkillRange = drRCToyCar.SkillRange;
        }
        //读取小车数据
        public int HP
        {
            get
            {
                return (int)m_HP;
            }
        }
        
        public float Speed
        {
            get
            {
                return m_Speed;
            }
        }
        
        public float AttackDamage
        {
            get
            {
                return m_AttackDamage;
            }
        }
        
        public float PropStorage
        {
            get
            {
                return m_PropStorage;
            }
        }
        
        public float SkillRange
        {
            get
            {
                return m_SkillRange;
            }
        }
        
        public float HitSound
        {
            get
            {
                return m_HitSound;
            }
        }
        
        public int DeadEffectId
        {
            get
            {
                return m_DeadEffectId;
            }
        }
        

    }
}