using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityGameFramework.Runtime;

namespace RCToyCar
{
    public class Missile : Entity
    {
        [SerializeField]
        private MissileData m_MissileData = null;
        
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            
        }
        
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_MissileData = userData as MissileData;
            if (m_MissileData == null)
            {
                Log.Error("Missile Data is invalid.");
                return;
            }
            


        }
        
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            CachedTransform.Translate(CachedTransform.rotation*Vector3.forward * m_MissileData.Speed * elapseSeconds, Space.World);
        }
    }
}