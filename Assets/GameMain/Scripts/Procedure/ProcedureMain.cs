//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace RCToyCar
{
    public class ProcedureMain : ProcedureBase
    {
        private const float GameOverDelayedSeconds = 3f;

        private readonly Dictionary<GameMode, GameBase> m_Games = new Dictionary<GameMode, GameBase>();
        private GameBase m_CurrentGame = null;
        private bool m_GotoMenu = false;
        private float m_GotoMenuDelaySeconds = 0f;

        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }

        public void GotoMenu()
        {
            m_GotoMenu = true;
        }

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
            m_Games.Add(GameMode.AIMode, new AIMode());
        }

        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
            m_Games.Clear();
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.UI.OpenUIForm(UIFormId.InGameUI);
            GameEntry.Event.Fire(this,new LoadingEventArgs(){LoadingSuccess = 1});
            m_GotoMenu = false;
            GameMode gameMode = (GameMode)procedureOwner.GetData<VarByte>("GameMode").Value;
            m_CurrentGame = m_Games[gameMode];
            m_CurrentGame.Initialize();

        }



        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (m_CurrentGame != null && !m_CurrentGame.GameOver)
            {
                m_CurrentGame.Update(elapseSeconds, realElapseSeconds);
                return;
            }
            
            

            if (!m_GotoMenu)
            {
                m_GotoMenu = true;
                m_GotoMenuDelaySeconds = 2f;

            }

            m_GotoMenuDelaySeconds += elapseSeconds;

            if (m_GotoMenuDelaySeconds >= GameOverDelayedSeconds)
            {
                procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Menu"));
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }
        
        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            if (m_CurrentGame != null)
            {
                m_CurrentGame.Shutdown();
                m_CurrentGame = null;

                GameEntry.Event.Fire(this,new GameOverEventArgs(){GameOverCloseUI = 1});
            }
           

            base.OnLeave(procedureOwner, isShutdown);
        }
        
    }
}
