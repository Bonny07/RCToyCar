//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2022-09-05 15:10:41.606
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace RCToyCar
{
    /// <summary>
    /// 道具属性表。
    /// </summary>
    public class DRProperties : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取道具编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取修理包回复血量。
        /// </summary>
        public int Healing
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取护盾抵挡伤害次数。
        /// </summary>
        public float PreventDamageTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取加速移速倍率。
        /// </summary>
        public float AddSpeed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取导弹伤害。
        /// </summary>
        public int MissileDamage
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取导弹持续时间。
        /// </summary>
        public float MissileLastTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取导弹飞行速度。
        /// </summary>
        public float MissileSpeed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取护盾持续时间。
        /// </summary>
        public float ShieldLastTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取加速持续时间。
        /// </summary>
        public float SpeedUpLastTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取导弹编号。
        /// </summary>
        public int MissileId
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            index++;
            Healing = int.Parse(columnStrings[index++]);
            PreventDamageTime = float.Parse(columnStrings[index++]);
            AddSpeed = float.Parse(columnStrings[index++]);
            MissileDamage = int.Parse(columnStrings[index++]);
            MissileLastTime = float.Parse(columnStrings[index++]);
            MissileSpeed = float.Parse(columnStrings[index++]);
            ShieldLastTime = float.Parse(columnStrings[index++]);
            SpeedUpLastTime = float.Parse(columnStrings[index++]);
            MissileId = int.Parse(columnStrings[index++]);

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    Healing = binaryReader.Read7BitEncodedInt32();
                    PreventDamageTime = binaryReader.ReadSingle();
                    AddSpeed = binaryReader.ReadSingle();
                    MissileDamage = binaryReader.Read7BitEncodedInt32();
                    MissileLastTime = binaryReader.ReadSingle();
                    MissileSpeed = binaryReader.ReadSingle();
                    ShieldLastTime = binaryReader.ReadSingle();
                    SpeedUpLastTime = binaryReader.ReadSingle();
                    MissileId = binaryReader.Read7BitEncodedInt32();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }
}
