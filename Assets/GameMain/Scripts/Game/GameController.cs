using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCToyCar
{
    public class GameController : MonoBehaviour
        //计时判断，时间归零则游戏结束
    {
        public static int GameResultNum; //游戏胜负判断
        public static float GameResultPlay;



        void Start()
        {
            GameResultNum = 0;
            GameResultPlay = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            if (RCCarHealth.CurrentHealth <= 0f && EnemyHealth.CurrentHealth > 0f)
            {
                GameResultNum--;
                GameResults();
            }

            //玩家被消灭，游戏判定失败
            if (EnemyHealth.CurrentHealth <= 0f && RCCarHealth.CurrentHealth > 0f)
            {
                GameResultNum++;
                GameResults();
            }

            //敌方被消灭，游戏判定胜利
            if (EnemyHealth.CurrentHealth <= 0f && RCCarHealth.CurrentHealth <= 0f)
            {
                GameResultNum = 0;
                GameResults();
            }

            //玩家与敌人同时被消灭,游戏判定平局
            if (TimeCountDownController.TimeisUp == 1 && EnemyHealth.CurrentHealth > 0f &&
                RCCarHealth.CurrentHealth > 0f)
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
        
        void GameResults()
        {
            if (GameResultNum == 0)
            {
                //判定游戏平局DRAW，播放平局UI，返回主菜单
                GameResultPlay = 2f;

            }

            if (GameResultNum > 0)
            {
                //判定游戏胜利Win，播放胜利UI，返回主菜单
                GameResultPlay = 1f;

            }

            if (GameResultNum < 0)
            {
                //判定游戏失败Fail，播放失败UI，返回主菜单
                GameResultPlay = 3f;

            }
        }
    }
}