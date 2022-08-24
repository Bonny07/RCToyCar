//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace RCToyCar
{
    public abstract class GameBase
    {
        private int CarNum = 0;

        public abstract GameMode GameMode
        {
            get;
        }

        public bool GameOver
        {
            get; 
            protected set;
        }
        
        public static int GameResultNum; //游戏胜负判断

        private MyRCToyCar m_MyRCToyCar = null;
        private AIRCToyCar m_AIRCToyCar = null;

        public virtual void Initialize()
        {
            GameOver = false;

            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            GameEntry.Event.Subscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);

            GameEntry.Entity.ShowMyRCToyCar(new MyRCToyCarData(GameEntry.Entity.GenerateSerialId(), 80001)
            {
                Name = "RCPlayerCar",
                Position = new Vector3(13, 2, -37),
            });

            var aiCar = new AIRCToyCarData(GameEntry.Entity.GenerateSerialId(), 80002);
            aiCar.Name = "RCAICar";
            aiCar.Position = new Vector3(-23, 2, -2.5f);
            
            GameEntry.Entity.ShowAIRCToyCar(aiCar);

            GameResultNum = 0;
        }


        public virtual void Shutdown()
        {
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            GameEntry.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);
        }

        public virtual void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (CarNum < 2)
            {
                return;
            }

            GameJudge();
        }

        protected virtual void OnShowEntitySuccess(object sender, GameEventArgs e)
        {
            ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
            if (ne.EntityLogicType == typeof(MyRCToyCar))
            {
                m_MyRCToyCar = (MyRCToyCar)ne.Entity.Logic;
                CarNum++;
            }
            else if (ne.EntityLogicType == typeof(AIRCToyCar))
            {
                m_AIRCToyCar = (AIRCToyCar)ne.Entity.Logic;
                CarNum++;
            }
        }

        protected virtual void OnShowEntityFailure(object sender, GameEventArgs e)
        {
            ShowEntityFailureEventArgs ne = (ShowEntityFailureEventArgs)e;
            Log.Warning("Show entity failure with error message '{0}'.", ne.ErrorMessage);
        }

        void GameJudge()
        {
            if (m_MyRCToyCar.IsDead && m_AIRCToyCar.IsDead)

            {
                GameResultNum = 0;
                GameResults();
                return;
            }
            //玩家与敌人同时被消灭,游戏判定平局

            if (m_AIRCToyCar.IsDead)
            {
                GameResultNum++;
                GameResults();
                return;
            }
            //敌方被消灭，游戏判定胜利

            if (m_MyRCToyCar.IsDead)
            {
                GameResultNum--;
                GameResults();
                return;
            }
            //玩家被消灭，游戏判定失败

            if (TimeCountDownController.TimeisUp == 1)
            {
                GameResultNum = 0;
                GameResults();
                return;
            }
            //游戏计时结束，游戏判定平局
        }

        void GameResults()
        {
            if (GameResultNum == 0)
            {
                //判定游戏平局DRAW，播放平局UI，返回主菜单
                GameEntry.Event.Fire(this, new GameResultEventArgs() { GameResultPlay = 2 });
                GameOver = true;
            }

            if (GameResultNum > 0)
            {
                //判定游戏胜利Win，播放胜利UI，返回主菜单
                GameEntry.Event.Fire(this, new GameResultEventArgs() { GameResultPlay = 1 });
                GameOver = true;
            }

            if (GameResultNum < 0)
            {
                //判定游戏失败Fail，播放失败UI，返回主菜单
                GameEntry.Event.Fire(this, new GameResultEventArgs() { GameResultPlay = 3 }); 
                GameOver = true;
            }
        } //游戏胜负播报
    }
}