using GameFramework.DataTable;
using System;
using UnityEngine;

namespace RCToyCar
{
    [Serializable]
    public class MissileData : EntityData
    {
        [SerializeField]
        private int m_OwnerId = 0;

        [SerializeField]
        private CampType m_OwnerCamp = CampType.Unknown;
        
        [SerializeField]
        private float m_LastTime = 0f;
        //导弹飞行时间
        
        [SerializeField]
        private float m_Speed = 0f;
        //导弹飞行速度
        
        public MissileData(int entityId, int typeId, int ownerId, CampType ownerCamp,float speed,float lasttime)
            : base(entityId, typeId)
        {
            m_OwnerId = ownerId;
            m_OwnerCamp = ownerCamp;
            m_LastTime = lasttime;
            m_Speed = speed;
        }

        public int OwnerId
        {
            get
            {
                return m_OwnerId;
            }
        }

        public CampType OwnerCamp
        {
            get
            {
                return m_OwnerCamp;
            }
        }
        
        public float LastTime
        {
            get
            {
                return m_LastTime;
            }
        }

        public float Speed
        {
            get
            {
                return m_Speed;
            }
        }
        
    }
}