using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace RCToyCar
{
    public class CarHealth : Entity
    {
        private const string AttachPoint = "Car Armor";
        [SerializeField]
        private ArmorData m_ArmorData = null;
        protected override void OnInit(object userData)
        {
            {
                base.OnInit(userData);
            }
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_ArmorData = userData as ArmorData;
            if (m_ArmorData == null)
            {
                Log.Error("Car Armor data is invalid.");
                return;
            }
            GameEntry.Entity.AttachEntity(Entity, m_ArmorData.OwnerId, AttachPoint);
        }

        protected override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
        {
            base.OnAttachTo(parentEntity, parentTransform, userData);
            Name = Utility.Text.Format("Car Armor of {0}", parentEntity.Name);
            CachedTransform.localPosition = Vector3.zero;
        }
    }
}