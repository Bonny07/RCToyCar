using GameFramework;
using GameFramework.DataTable;
using UnityEngine;

namespace RCToyCar
{
    public class AIMode : GameBase
    {
        public static float GameResultPlay = 0f;
        
        public override GameMode GameMode
        {
            get
            {
                return GameMode.AIMode;
            }
        }

        

    }
}
    
