using GameFramework.Event;


namespace RCToyCar
{
    public class GameResultEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(GameResultEventArgs).GetHashCode();
        public int GameResultPlay = 0;

        public override void Clear()
        {
            GameResultPlay = 0;
        }

        public override int Id => EventId;
    }
}