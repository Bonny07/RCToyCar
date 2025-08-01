using System;
using UnityEngine;

namespace RCToyCar
{
    [Serializable]
    public class MyRCToyCarData : RCToyCarData
    {
        [SerializeField]
        private string m_Name = null;

        public MyRCToyCarData(int entityId, int typeId)
            : base(entityId, typeId, CampType.Player)
        {
        }

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
    }
}