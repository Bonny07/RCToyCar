using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RCToyCar
{
    public class TimeCountDownController : MonoBehaviour
    {
        public float CountDown; //游戏计时器
        public TextMeshProUGUI CountDownText;
        public static int TimeisUp; //游戏时间是否结束


        void Start()
        {
            CountDown = 90f;
            TimeisUp = 0;
        }
        
        void Update()
        {
            CountDown -= Time.deltaTime;
            CountDownText.text = (int)CountDown + "s";
            GameTimer();
        }
        void GameTimer()
        {
            if (CountDown <= 0f || GameController.GameResultNum != 0)
            {
                TimeisUp = 1;
                CountDown += Time.deltaTime;
            }
        }
    }
}