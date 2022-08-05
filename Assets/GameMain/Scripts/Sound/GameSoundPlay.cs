using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCToyCar
{
    public class GameSoundPlay : MonoBehaviour
    {
        private int playtime;
                
        public AudioClip WinSound;
        public AudioClip DrawSound;
        public AudioClip LoseSound;



        // Start is called before the first frame update
        void Start()
        {
            playtime = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameController.GameResultPlay != 0&&playtime==0)
            {
                ResultSound();
            }
        }
        
        void ResultSound()
        {

            if (GameController.GameResultNum > 0)
            {

                AudioSource.PlayClipAtPoint(WinSound, transform.position);
                playtime++;
            }
            if (GameController.GameResultNum == 0)
            {
                AudioSource.PlayClipAtPoint(DrawSound, transform.position);
                playtime++;
            }
            if (GameController.GameResultNum < 0)
            {
                AudioSource.PlayClipAtPoint(LoseSound, transform.position);
                playtime++;
            }
        }
    }
}