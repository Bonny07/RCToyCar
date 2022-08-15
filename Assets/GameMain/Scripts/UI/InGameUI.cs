using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameFramework.DataTable;

namespace RCToyCar
{
    public class InGameUI : UGuiForm
    {
        private ProcedureMain m_ProcedureMain = null;


        protected override void OnInit(object userData)
        {

        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_ProcedureMain = (ProcedureMain)userData;
            
            if (m_ProcedureMain == null)
            {
                Log.Warning("ProcedureMain is invalid when open Gaming.");
                return;
            }
            // 换个音乐
            GameEntry.Sound.PlayMusic(4);
        }
        //游戏开始打开游戏内UI

        protected override void OnClose(bool isShutdown, object userData)
        {
            m_ProcedureMain = null;
            base.OnClose(isShutdown, userData);
            /*GameEntry.UI.CloseUIForm(this);*/

            // 还原音乐
            GameEntry.Sound.PlayMusic(1);
        }
        //游戏结束关闭游戏内UI
        
        
        

        public void OnSettingButtonClick()
        {
            
        }
        //按下设置按钮
        public void OnUserButtonClick()
        {
            
        }
        //按下用户按钮
        public void OnQuestionButtonClick()
        {
            
        }
        //按下问号按钮
        public void OnMuteButtonClick()
        {
            
        }
        //按下静音按钮
        public void OnQuitButtonClick()
        {
            
            
        }
        //按下返回大厅按钮
    }
}