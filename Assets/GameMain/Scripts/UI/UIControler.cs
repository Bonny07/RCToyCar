using GameFramework.Event;
using System.Collections.Generic;
using UnityEngine;

namespace RCToyCar
{
    public class UIControler : MonoBehaviour
    {
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

        private void Start()
        {
            GameResultPlay = 0;
            GameResultWin.SetActive(false);
            GameResultDraw.SetActive(false);
            GameResultLose.SetActive(false);
            GameEntry.Event.Subscribe(GameResultEventArgs.EventId,GameResultPlayController);
            GameEntry.Event.Subscribe(SkillStorageEventArgs.EventId,PropStorageController);
            StorageShield = 0;
            StorageSpeedup = 0;
            StorageMissile = 0;
        }

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


        void Update()
        {
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
    }
}