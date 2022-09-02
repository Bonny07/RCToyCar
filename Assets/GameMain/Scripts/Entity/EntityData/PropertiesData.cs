using GameFramework.DataTable;
using System;
using UnityEngine;

namespace RCToyCar
{
    [Serializable]
    public class PropertiesData : AccessoryObjectData
    {
        [SerializeField]
        private int m_MissileId = 0;
        
        public float m_MissileSpeed = 0;
        //飞行速度
        
        public float m_MissileLastTime = 0;
        //导弹持续时间


        public PropertiesData(int entityId, int typeId, int ownerId, CampType ownerCamp)
            : base(entityId, typeId, ownerId, ownerCamp)
        {
            IDataTable<DRProperties> dtProperties = GameEntry.DataTable.GetDataTable<DRProperties>();
            DRProperties drProperties = dtProperties.GetDataRow(TypeId);
            if (drProperties == null)
            {
                return;
            }
            m_MissileLastTime = drProperties.MissileLastTime;
            m_MissileSpeed = drProperties.MissileSpeed;
            m_MissileId = drProperties.MissileId;
        }
        //读取道具数据

        public float MissileSpeed
        {
            get { return m_MissileSpeed; }
        }

        public float MissileLastTime
        {
            get { return m_MissileLastTime; }
        }

        public int MissileId
        {
            get { return m_MissileId; }
        }
    }
}