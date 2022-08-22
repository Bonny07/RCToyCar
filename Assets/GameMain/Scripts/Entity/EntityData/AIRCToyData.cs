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
            IDataTable<DRWayPoint> dtPositionNum = GameEntry.DataTable.GetDataTable<DRWayPoint>();
            DRWayPoint drPositionNum = dtPositionNum.GetDataRow(10000);
            if (drPositionNum == null)
            {
                return;
            }
            m_RandomNumRange = drPositionNum.WayPointNum;

            for (int n = 1; n < RandomNumRange; n++)
            {
                IDataTable<DRWayPoint> dtPosition = GameEntry.DataTable.GetDataTable<DRWayPoint>();
                DRWayPoint drPosition = dtPosition.GetDataRow(n);
                if (drPosition == null)
                {
                    return;
                }
                m_WayPointPosition.Add(drPosition.Position);
                
            }
        }

        [SerializeField]
        public int m_RandomNumRange;
        //随机最大范围

        [SerializeField] private List<Vector3> m_WayPointPosition = new List<Vector3>();
        //路径点坐标
        
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

        public List<Vector3> WayPointPosition
        {
            get
            {
                return m_WayPointPosition;
            }
        }

        public int RandomNumRange
        {
            get
            {
                return m_RandomNumRange;
            }
        }
        
        public override int MaxHP { get; }
        

    }
}