using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControler : MonoBehaviour
{
    public GameObject SkillShield;
    public GameObject SkillSpeed;
    public GameObject SkillMissile;

    void Update()
    {
        if (Skill.Shield == 1)
        {
            SkillShield.SetActive(true);
        }
        if (Skill.Shield != 1)
        {
            SkillShield.SetActive(false);
        }
        if (Skill.Speedup == 1)
        {
            SkillSpeed.SetActive(true);
        }
        if (Skill.Speedup != 1)
        {
            SkillSpeed.SetActive(false);
        }
        if (Skill.Missile == 1)
        {
            SkillMissile.SetActive(true);
        }
        if (Skill.Missile != 1)
        {
            SkillMissile.SetActive(false);
        }
    }
}
