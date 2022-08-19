using GameFramework;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace RCToyCar
{
    public abstract class RCTOYCar : TargetableObject
    {
        [SerializeField]
        private RCToyCarData m_RcToyCarData = null;
        
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_RcToyCarData = userData as RCToyCarData;
            if (m_RcToyCarData == null)
            {
                Log.Error("RcToyCarData data is invalid.");
                return;
            }

            Name = Utility.Text.Format("RcToyCar ({0})", Id);
        }
        
        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
        }
        
        protected override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
        {
            base.OnAttached(childEntity, parentTransform, userData);
        }
        
        protected override void OnDetached(EntityLogic childEntity, object userData)
        {
            base.OnDetached(childEntity, userData);
        }
        
        protected override void OnDead(Entity attacker)
        {
            base.OnDead(attacker);

            GameEntry.Entity.ShowEffect(new EffectData(GameEntry.Entity.GenerateSerialId(), m_RcToyCarData.DeadEffectId)
            {
                Position = CachedTransform.localPosition,
            });
        }
        
        public override ImpactData GetImpactData()
        {
            return new ImpactData(m_RcToyCarData.Camp, m_RcToyCarData.HP, 0, m_RcToyCarData.DeadEffectId);
        }
    }
}