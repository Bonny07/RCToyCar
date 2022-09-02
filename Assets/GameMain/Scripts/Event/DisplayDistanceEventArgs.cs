using GameFramework.Event;

namespace RCToyCar
{
    public class DisplayDistanceEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(DisplayDistanceEventArgs).GetHashCode();
        public float DisplayDistance;
        
        public override void Clear()
        {
            DisplayDistance = 0f;
        }

        public override int Id => EventId;
    }
}
