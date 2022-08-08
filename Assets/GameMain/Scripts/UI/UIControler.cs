using System;
using System.Collections;
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

            if (GameManager.GameResultPlay == 1f)
            {
                GameResultWin.SetActive(true);
            }

            if (GameManager.GameResultPlay == 2f)
            {
                GameResultDraw.SetActive(true);
            }

            if (GameManager.GameResultPlay == 3f)
            {
                GameResultLose.SetActive(true);
            }
        }
    }
}