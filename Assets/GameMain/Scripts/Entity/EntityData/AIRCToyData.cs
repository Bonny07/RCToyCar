using GameFramework.DataTable;
using System;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.DataTable;

namespace RCToyCar
{
    [Serializable]
    public class AIRCToyCarData : RCToyCarData
    {
        
        [SerializeField]
        private string m_Name = null;

        public AIRCToyCarData(int entityId, int typeId)
            : base(entityId, typeId, CampType.Enemy)
        {
            
        }

        [SerializeField]
        private Vector3 m_WayPointPosition;
        //路径点坐标
        
        private int RandomNumRange;

        
        public AIRCToyCarData(int entityId, int typeId, CampType camp)
            : base(entityId, typeId, camp)
        {
            IDataTable<DRWayPoint> dtPositionNum = GameEntry.DataTable.GetDataTable<DRWayPoint>();
            DRWayPoint drPositionNum = dtPositionNum.GetDataRow(10000);
            if (drPositionNum == null)
            {
                return;
            }
            RandomNumRange = drPositionNum.WayPointNum;

            for (int n = 1; n < RandomNumRange; n++)
            {
                IDataTable<DRWayPoint> dtPosition = GameEntry.DataTable.GetDataTable<DRWayPoint>();
                DRWayPoint drPosition = dtPosition.GetDataRow(n);
                if (drPosition == null)
                {
                    return;
                }

                m_WayPointPosition = drPosition.Position;
                new List<Vector3>();
            }
        }
        //读取小车数据
        
        /// <summary>
        /// 角色名称。
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public Vector3 WayPointPosition
        {
            get
            {
                return m_WayPointPosition;
            }
        }
        
        public override int MaxHP { get; }

    }
}