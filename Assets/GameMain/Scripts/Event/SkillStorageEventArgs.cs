using GameFramework.Event;

namespace RCToyCar
{
    public class SkillStorageEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(SkillStorageEventArgs).GetHashCode();
        public int ShieldStorage;
        public int SpeedUpStorage;
        public int MissileStorage;

        public override void Clear()
        {
            ShieldStorage = 0;
            SpeedUpStorage = 0;
            MissileStorage = 0;
        }

        public override int Id => EventId;
    }
}
