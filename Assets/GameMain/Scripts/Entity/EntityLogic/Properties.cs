using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace RCToyCar
{
    [SerializeField]
    public class Properties : Entity
    {
        private const string AttachPoint = "Fire Transform";

        [SerializeField] 
        private PropertiesData m_PropertiesData = null;
        
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_PropertiesData = userData as PropertiesData;
            if (m_PropertiesData == null)
            {
                Log.Error("Properties data is invalid.");
                return;
            }

            GameEntry.Entity.AttachEntity(Entity, m_PropertiesData.OwnerId, AttachPoint); //组合

            
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        }
        

        protected override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
        {
            base.OnAttachTo(parentEntity, parentTransform, userData);

            Name = Utility.Text.Format("Weapon of {0}", parentEntity.Name);
            CachedTransform.localPosition = Vector3.zero;
        }


        public void MissilePush()
        {
            GameEntry.Entity.ShowMissile(new MissileData(GameEntry.Entity.GenerateSerialId(), m_PropertiesData.MissileId, m_PropertiesData.OwnerId, m_PropertiesData.OwnerCamp, m_PropertiesData.MissileSpeed,m_PropertiesData.MissileLastTime)
            {
                Position = CachedTransform.position,Rotation=CachedTransform.rotation
            });
        }

    }
}