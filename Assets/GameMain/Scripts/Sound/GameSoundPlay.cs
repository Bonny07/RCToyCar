using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCToyCar
{
    public class GameSoundPlay : MonoBehaviour
    {
        private int playtime;
        

        // Start is called before the first frame update
        void Start()
        {
            playtime = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.GameResultPlay != 0&&playtime==0)
            {
                ResultSound();
            }
        }
        
        void ResultSound()
        {

            if (GameManager.GameResultNum > 0)
            {
                GameEntry.Sound.PlaySound(40000);
                playtime++;
            }
            if (GameManager.GameResultNum == 0)
            {
                GameEntry.Sound.PlaySound(40001);
                playtime++;
            }
            if (GameManager.GameResultNum < 0)
            {
                GameEntry.Sound.PlaySound(40002);
                playtime++;
            }
        }
    }
}