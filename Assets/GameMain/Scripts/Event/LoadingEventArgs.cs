using GameFramework.Event;

namespace RCToyCar
{
    public class LoadingEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(LoadingEventArgs).GetHashCode();
        public int LoadingSuccess = 0;

        public override void Clear()
        {
            LoadingSuccess = 0;
        }

        public override int Id => EventId;
    }
}