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

        private void Start()
        {
            GameResultPlay = 0;
            GameEntry.Event.Subscribe(GameResultEventArgs.EventId,GameResultPlayController);
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
            if (ne.GameResultPlay == 2)
            {
                GameResultPlay = 2;
            }
            if (ne.GameResultPlay == 3)
            {
                GameResultPlay = 3;
            }
        }


        void Update()
        {
            if (PlayerSkill.Shield == 1)
            {
                SkillShield.SetActive(true);
            }

            if (PlayerSkill.Shield != 1)
            {
                SkillShield.SetActive(false);
            }

            if (PlayerSkill.Speedup == 1)
            {
                SkillSpeed.SetActive(true);
            }

            if (PlayerSkill.Speedup != 1)
            {
                SkillSpeed.SetActive(false);
            }

            if (PlayerSkill.Missile == 1)
            {
                SkillMissile.SetActive(true);
            }

            if (PlayerSkill.Missile != 1)
            {
                SkillMissile.SetActive(false);
            }

            if (GameResultPlay == 1)
            {
                GameResultWin.SetActive(true);
            }

            if (GameResultPlay == 2)
            {
                GameResultDraw.SetActive(true);
            }

            if (GameResultPlay == 3)
            {
                GameResultLose.SetActive(true);
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