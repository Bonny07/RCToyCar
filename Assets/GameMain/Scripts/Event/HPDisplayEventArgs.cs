using GameFramework.Event;

namespace RCToyCar
{
    public class PlayerHPDisplayEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(PlayerHPDisplayEventArgs).GetHashCode();
        public int PlayerHP = 0;


        public override void Clear()
        {
            PlayerHP = 0;

        }

        public override int Id => EventId;
    }
}