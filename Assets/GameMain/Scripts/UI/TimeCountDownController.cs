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
        private RCToyCarData m_MyRcToyCarData = null;
        private AIRCToyCarData m_AIRCToyCarData = null;


        void Start()
        {
            CountDown = 90f;
            TimeisUp = 0;
        }
        
        void Update()
        {
            CountDown -= Time.deltaTime;
            CountDownText.text = (int)CountDown + "s";
            /*PlayerHP.text = m_MyRcToyCarData.HP + "Player";
            EnemyHP.text = m_AIRCToyCarData.HP + "Enemy";*/
            GameTimer();
        }
        void GameTimer()
        {
            if (CountDown <= 0f || GameBase.GameResultNum != 0)
            {
                TimeisUp = 1;
                CountDown += Time.deltaTime;
            }
        }
    }
}