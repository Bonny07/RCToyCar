using System.Collections;
using GameFramework.Event;
using UnityEngine;

namespace RCToyCar
{
    public class CamControlEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(CamControlEventArgs).GetHashCode();
        public float CamHorizontal = 0f;
        public float CamVertical = 0f;
        
        public override void Clear()
        {
            CamHorizontal = 0f;
            CamVertical = 0f;
        }

        public override int Id => EventId;
    }
}