using GameFramework.Event;

namespace RCToyCar
{
    public class LoadingUi : UGuiForm
    {
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            GameEntry.Event.Subscribe(LoadingEventArgs.EventId,LoadingUIControl);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }
        //加载结束关闭加载UI

        void LoadingUIControl(object sender, GameEventArgs e)
        {
            LoadingEventArgs ne = (LoadingEventArgs)e;
            if (ne.LoadingSuccess ==  0)
            {
                return;
            }
            Invoke("CloseLoadingUI",2f);
        }

        public void CloseLoadingUI()
        {
            Close();
            gameObject.SetActive(false);

        }


    }
}