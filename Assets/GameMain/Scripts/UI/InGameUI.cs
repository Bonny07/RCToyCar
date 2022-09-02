using System;
using UnityEngine;
using GameFramework.Event;
using TMPro;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameFramework.DataTable;

namespace RCToyCar
{
    public class InGameUI : UGuiForm
    {
        [SerializeField]
        private ProcedureMain m_ProcedureMain = null;
        
        public GameObject SkillShield;
        public GameObject SkillSpeed;
        public GameObject SkillMissile;
        public GameObject GameResultWin;
        public GameObject GameResultDraw;
        public GameObject GameResultLose;
        private int GameResultPlay;
        public int StorageShield;
        public int StorageSpeedup;
        public int StorageMissile;
        public float CountDown; //游戏计时器
        public TextMeshProUGUI CountDownText;
        public static int TimeisUp; //游戏时间是否结束
        private RCToyCarData m_MyRcToyCarData = null;
        private AIRCToyCarData m_AIRCToyCarData = null;
        private bool isOn;
        public Slider m_testSlider;


        protected override void OnInit(object userData)
        {
            base.OnInit(userData);


        }

        protected override void OnOpen(object userData)
        {
            m_ProcedureMain = new ProcedureMain();
            base.OnOpen(userData);

            GameEntry.Event.Subscribe(GameResultEventArgs.EventId,GameResultPlayController);
            GameEntry.Event.Subscribe(SkillStorageEventArgs.EventId,PropStorageController);   
            GameEntry.Event.Subscribe(GameOverEventArgs.EventId,CloseUI);
            m_testSlider.onValueChanged.AddListener(OnNumSilderChange);

            StorageShield = 0;
            StorageSpeedup = 0;
            StorageMissile = 0;
            GameResultPlay = 0;
            GameResultWin.SetActive(false);
            GameResultDraw.SetActive(false);
            GameResultLose.SetActive(false); 
            CountDown = 90f;
            TimeisUp = 0;
            isOn = false;
        }
        //游戏开始打开游戏内UI

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds,realElapseSeconds);
            CountDown -= Time.deltaTime;
            CountDownText.text = (int)CountDown + "s";
            GameTimer();
            
            if (StorageShield == 1)
            {
                SkillShield.SetActive(true);
            }

            else if (StorageShield != 1)
            {
                SkillShield.SetActive(false);
            }

            if (StorageSpeedup == 1)
            {
                SkillSpeed.SetActive(true);
            }

            else if (StorageSpeedup != 1)
            {
                SkillSpeed.SetActive(false);
            }

            if (StorageMissile == 1)
            {
                SkillMissile.SetActive(true);
            }

            else if (StorageMissile != 1)
            {
                SkillMissile.SetActive(false);
            }

            if (GameResultPlay == 1)
            {
                GameResultWin.SetActive(true);
                GameResultDraw.SetActive(false);
                GameResultLose.SetActive(false);

            }

            else if (GameResultPlay == 2)
            {
                GameResultDraw.SetActive(true);
                GameResultWin.SetActive(false);
                GameResultLose.SetActive(false);

            }

            else if (GameResultPlay == 3)
            {
                GameResultLose.SetActive(true);
                GameResultWin.SetActive(false);
                GameResultDraw.SetActive(false);

            }
            else
            {
                GameResultWin.SetActive(false);
                GameResultDraw.SetActive(false);
                GameResultLose.SetActive(false);
                
            }
        }
        
        void GameTimer()
        {
            if (CountDown <= 0f || GameBase.GameResultNum != 0)
            {
                TimeisUp = 1;
                CountDown += Time.deltaTime;
            }
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            GameEntry.Event.Unsubscribe(GameOverEventArgs.EventId,CloseUI);
            GameEntry.Event.Unsubscribe(GameResultEventArgs.EventId,GameResultPlayController);
            GameEntry.Event.Unsubscribe(SkillStorageEventArgs.EventId,PropStorageController);
            m_ProcedureMain = null;
            base.OnClose(isShutdown, userData);
        }
        //游戏结束关闭游戏内UI


        private void OnNumSilderChange(float value)
        {
            GameEntry.Event.Fire(this,new DisplayDistanceEventArgs(){DisplayDistance = value});
        }

        
        
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
        public void OnMuteButtonClick(bool isOn)
        {
            GameEntry.Sound.Mute("Music", isOn);
        }
        //按下静音按钮
        public void OnQuitButtonClick()
        {
            
            
        }
        //按下返回大厅按钮
        
        private void PropStorageController(object sender, GameEventArgs e)
        {
            SkillStorageEventArgs ne = (SkillStorageEventArgs)e;
            if (ne.ShieldStorage ==  0&&ne.SpeedUpStorage==0&&ne.MissileStorage==0)
            {
                return;
            }

            if (ne.ShieldStorage == 1)
            {
                StorageShield=1;
                ne.ShieldStorage = 0;
            }
            else if (ne.SpeedUpStorage == 1)
            {
                StorageSpeedup=1;
                ne.SpeedUpStorage = 0;
            }
            else if (ne.MissileStorage == 1)
            {
                StorageMissile=1;
                ne.MissileStorage = 0;
            }
            else if (ne.ShieldStorage == 2)
            {
                StorageShield=0;
                ne.ShieldStorage = 0;
            }
            else if (ne.SpeedUpStorage == 2)
            {
                StorageSpeedup=0;
                ne.SpeedUpStorage = 0;
            }
            else if (ne.MissileStorage == 2)
            {
                StorageMissile=0;
                ne.MissileStorage = 0;
            }
        }
        private void GameResultPlayController(object sender, GameEventArgs e)
        {
            GameResultEventArgs ne = (GameResultEventArgs)e;
            if (ne.GameResultPlay ==  0)
            {
                return;
            }
            if (ne.GameResultPlay == 1)
            {
                GameResultPlay = 1;
            }
            else if (ne.GameResultPlay == 2)
            {
                GameResultPlay = 2;
            }
            else if (ne.GameResultPlay == 3)
            {
                GameResultPlay = 3;
            }
        }
        
        public void SkillShiledUsing()
        {
            GameEntry.Event.Fire(this, new SkillEventArgs(){Skillnum = 1});
        }
        //抛出护盾使用事件
        public void SkillSPeedUpUsing()
        {
            GameEntry.Event.Fire(this,new SkillEventArgs(){Skillnum = 2});
        }
        //抛出加速使用事件
        public void SkillMissileUsing()
        {
            GameEntry.Event.Fire(this,new SkillEventArgs(){Skillnum = 3});
        }
        //抛出导弹使用事件

        public void CloseUI(object sender, GameEventArgs e)
        {
            GameOverEventArgs ne = (GameOverEventArgs)e;
            if (ne.GameOverCloseUI ==  1)
            {
                StorageShield = 0;
                StorageSpeedup = 0;
                StorageMissile = 0;
                GameResultPlay = 0;
                GameResultWin.SetActive(false);
                GameResultDraw.SetActive(false);
                GameResultLose.SetActive(false); 
                CountDown = 90f;
                TimeisUp = 0;
                isOn = false;
                Close();
                return;
            }
        }
    }
}