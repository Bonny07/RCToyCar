using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCToyCar
{
    public class BGMPlay : MonoBehaviour
    {
        public GameObject BGM;


        void Start()
        {
            BGM.SetActive(true);
        }


        void Update()
        {
            BGMContinue();
        }
        
        void BGMContinue()
        {
            if (GameManager.GameResultNum != 0f)
            {
                BGM.SetActive(false);
            }
        }
    }
}