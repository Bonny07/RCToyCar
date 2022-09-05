using GameFramework.Event;

namespace RCToyCar
{
    public class AIHPDisplayEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(AIHPDisplayEventArgs).GetHashCode();
        public int AIHP = 0;
        public override void Clear()
        {
            AIHP = 0;
        }

        public override int Id => EventId;
    }
}