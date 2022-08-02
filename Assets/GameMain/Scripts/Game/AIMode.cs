using GameFramework;
using GameFramework.DataTable;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.Event;

namespace StarForce
{
    public class AIMode : MonoBehaviour
    {
        public float CountDown; //游戏计时器
        private int GameResultNum; //游戏胜负判断
        private int TimeisUp;  //游戏时间是否结束
        public static float GameResultPlay=0f;

        void Start()
        {
            GameResultNum = 0;
            TimeisUp = 1;
            CountDown = 90f;
        }


        void Update()
        {
            CountDown-=Time.deltaTime;
            GameTimer();
            //计时判断，时间归零则游戏结束
        }

        void GameTimer()
        {
            if (CountDown <= 0f || GameResultNum != 0)
            {
                TimeisUp = 1;
                GameController();
                //胜负控制
            }

            if (CountDown > 0f && GameResultNum != 0)
            {
                GameController();
            }
            
        }

         void GameController()
        {
            if (RCCarHealth.CurrentHealth <= 0&&EnemyHealth.CurrentHealth>0)
            {
                GameResultNum--;
                GameResults();
            }
            //玩家被消灭，游戏判定失败
            if (EnemyHealth.CurrentHealth <= 0&&RCCarHealth.CurrentHealth>0)
            {
                GameResultNum++;
                GameResults();
            }
            //敌方被消灭，游戏判定胜利
            if (EnemyHealth.CurrentHealth <= 0 && RCCarHealth.CurrentHealth <= 0)
            {
                GameResultNum = 0;
                GameResults();
            }
            //玩家与敌人同时被消灭,游戏判定平局
            if (TimeisUp==1&&EnemyHealth.CurrentHealth>0&&RCCarHealth.CurrentHealth>0)
            {
                if (EnemyHealth.CurrentHealth >= RCCarHealth.CurrentHealth)
                {
                    GameResultNum = 0;
                    GameResults();

                }
                //玩家血量大于敌方，且游戏计时结束，游戏判定平局
                if (EnemyHealth.CurrentHealth < RCCarHealth.CurrentHealth)
                {
                    GameResultNum++;
                    GameResults();
                }
                //玩家血量小于等于敌方，且游戏计时结束，游戏判定胜利
            }


        }
        //游戏胜负判定



        void GameResults()
        {
            if (GameResultNum == 0)
            {
                //判定游戏平局DRAW，播放平局UI，返回主菜单
                GameResultPlay = 2f;
                Time.timeScale = 0;
            }

            if (GameResultNum > 0)
            {
                //判定游戏胜利Win，播放胜利UI，返回主菜单
                GameResultPlay = 1f;
                Time.timeScale = 0;
            }

            if (GameResultNum < 0)
            {
                //判定游戏失败Fail，播放失败UI，返回主菜单
                GameResultPlay = 3f;
                Time.timeScale = 0;
            }
        }
        //游戏结算
    }
    
}