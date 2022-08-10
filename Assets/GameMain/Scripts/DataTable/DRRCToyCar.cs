//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2022-08-10 10:44:35.576
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
    /// 小车属性表。
    /// </summary>
    public class DRRCToyCar : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取小车编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取血量。
        /// </summary>
        public float MaxHP
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取速度(玩家敌人标准不同）。
        /// </summary>
        public float Speed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取普通攻击伤害。
        /// </summary>
        public float CarAttackDamage
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取道具存储上限制。
        /// </summary>
        public int PropStorage
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取敌人道具释放时间（1，最大值-1）。
        /// </summary>
        public float SkillRange
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
            MaxHP = float.Parse(columnStrings[index++]);
            Speed = float.Parse(columnStrings[index++]);
            CarAttackDamage = float.Parse(columnStrings[index++]);
            PropStorage = int.Parse(columnStrings[index++]);
            SkillRange = float.Parse(columnStrings[index++]);

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
                    MaxHP = binaryReader.ReadSingle();
                    Speed = binaryReader.ReadSingle();
                    CarAttackDamage = binaryReader.ReadSingle();
                    PropStorage = binaryReader.Read7BitEncodedInt32();
                    SkillRange = binaryReader.ReadSingle();
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
