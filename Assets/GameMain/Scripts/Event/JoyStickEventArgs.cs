using GameFramework.Event;
using UnityEngine;

namespace RCToyCar
{
    public class JoyStickEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(JoyStickEventArgs).GetHashCode();
        public float Horizontal = 0f;
        public float Vertical = 0f;
        
        public override void Clear()
        {
            Horizontal = 0f;
            Vertical = 0f;
        }

        public override int Id => EventId;
    }
}