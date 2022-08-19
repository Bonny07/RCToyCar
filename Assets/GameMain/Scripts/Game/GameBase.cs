//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Event;
using UnityEngine;
using UnityEngine.UIElements;
using UnityGameFramework.Runtime;

namespace RCToyCar
{
    public abstract class GameBase
    {
        public abstract GameMode GameMode
        {
            get;
        }

        public bool GameOver
        {
            get;
            protected set;
        }

        private MyRCToyCar m_MyRCToyCar = null;

        public virtual void Initialize()
        {
            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            GameEntry.Event.Subscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);
            
            GameEntry.Entity.ShowMyRCToyCar(new MyRCToyCarData(GameEntry.Entity.GenerateSerialId(), 80001)
            {
                Name = "My RCToyCar",
                Position = new Vector3(13,2,-37),
            });

            GameEntry.Entity.ShowAIRCToyCar(new AIRCToyCarData(GameEntry.Entity.GenerateSerialId(), 80002)
            {
                Name = "AI RCToyCar",
                Position = new Vector3(-23,2,-2.5f),
            });

            GameOver = false;
            m_MyRCToyCar = null;
        }

        public virtual void Shutdown()
        {
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            GameEntry.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);
        }

        public virtual void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (GameManager.GameResultPlay != 0f)
            {
                GameOver = true;
                return;
            }
        }

        protected virtual void OnShowEntitySuccess(object sender, GameEventArgs e)
        {
            ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
            if (ne.EntityLogicType == typeof(MyRCToyCar))
            {
                m_MyRCToyCar = (MyRCToyCar)ne.Entity.Logic;
            }
        }

        protected virtual void OnShowEntityFailure(object sender, GameEventArgs e)
        {
            ShowEntityFailureEventArgs ne = (ShowEntityFailureEventArgs)e;
            Log.Warning("Show entity failure with error message '{0}'.", ne.ErrorMessage);
        }
    }
}
