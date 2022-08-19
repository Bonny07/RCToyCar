using GameFramework.DataTable;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RCToyCar
{
    [Serializable]
    public class PropertiesData : AccessoryObjectData
    {
        [SerializeField]
        private float m_Healing = 0;
        //回复血量
        
        [SerializeField]
        private float m_PreventDamageTime = 0;
        //抵挡伤害次数
                
        [SerializeField]
        private float m_AddSpeed = 0;
        //移速倍率
                
        [SerializeField]
        private float m_MisslieDamage = 0;
        //导弹伤害
                
        [SerializeField]
        private float m_LastTime = 0;
        //持续时间
                
        [SerializeField]
        private float m_MissileSpeed = 0;
        //飞行速度

        public PropertiesData(int entityId, int typeId, int ownerId, CampType ownerCamp) : base(entityId, typeId, ownerId, ownerCamp)
        {
            IDataTable<DRProperties> dtProperties = GameEntry.DataTable.GetDataTable<DRProperties>();
            DRProperties drProperties = dtProperties.GetDataRow(TypeId);
            if (drProperties == null)
            {
                return;
            }

            m_Healing = drProperties.Healing;
            m_PreventDamageTime = drProperties.PreventDamageTime;
            m_AddSpeed = drProperties.AddSpeed;
            m_MisslieDamage = drProperties.MissileDamage;
            m_LastTime = drProperties.LastTime;
            m_MissileSpeed = drProperties.MissileSpeed;
        }
        //读取道具数据
        
        public float Healing
        {
            get
            {
                return m_Healing;
            }
        }

        public float PreventDamageTime
        {
            get
            {
                return m_PreventDamageTime;
            }
        }

        public float AddSpeed
        {
            get
            {
                return m_AddSpeed;
            }
        }

        public float MisslieDamage
        {
            get
            {
                return m_MisslieDamage;
            }
        }
        
        public float LastTime
        {
            get
            {
                return m_LastTime;
            }
        }
        
        public float MissileSpeed
        {
            get
            {
                return m_MissileSpeed;
            }
        }
    }
}