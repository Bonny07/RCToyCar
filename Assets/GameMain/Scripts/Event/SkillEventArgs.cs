using GameFramework.Event;

namespace RCToyCar
{
    public class SkillEventArgs : GameEventArgs
    {

        public static readonly int EventId = typeof(SkillEventArgs).GetHashCode();
        public int Skillnum = 0;

        public override void Clear()
        {
            Skillnum = 0;
        }

        public override int Id => EventId;
    }
}