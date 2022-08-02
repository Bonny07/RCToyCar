using GameFramework;
using GameFramework.Fsm;
using GameFramework.Procedure;
using System;
using System.Collections;
using UnityEngine;

public class AIMode : MonoBehaviour
{
    private int CountDown;  //游戏计时器
    private int GameResultNum;  //游戏胜负判断

    void Start()
    {
        CountDown = 90;
        GameResultNum = 0;
    }


    void Update()
    {
        Timer();
    }

    void Timer()
    {
        while (CountDown != 0&&GameResultNum==0)
        {
            CountDown--;
            if (CountDown <= 0||GameResultNum!=0)
            {
                GameOver();
                break;
            }
        }

        GameResults();
    }

    void GameOver()
    {
        /*if(Movement.)*/
    }

    void GameResults()
    {
        CountDown = 0;
        if (GameResultNum == 0)
        {
            //判定游戏平局DRAW，播放平局UI，返回主菜单
            /**/
        }

        if (GameResultNum > 0)
        {
            //判定游戏胜利Win，播放胜利UI，返回主菜单
        }

        if (GameResultNum < 0)
        {
            //判定游戏失败Fail，播放失败UI，返回主菜单
        }
    }
}
