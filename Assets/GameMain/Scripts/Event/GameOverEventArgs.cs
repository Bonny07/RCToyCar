using GameFramework.Event;

namespace RCToyCar
{
    public class GameOverEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(GameOverEventArgs).GetHashCode();
        public int GameOverCloseUI;
        
        public override void Clear()
        {
            GameOverCloseUI = 0;
        }

        public override int Id => EventId;
    }
}