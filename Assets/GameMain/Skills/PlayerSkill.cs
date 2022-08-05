using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCToyCar
{
    public class PlayerSkill : MonoBehaviour
    {
        public static float Shield = 0;
        public static float Speedup = 0;
        public static float Missile = 0;
        public static float ShieldActive = 0f;
        public GameObject SkillShield;
        
        public AudioClip ShieldActivate;
        public AudioClip SpeedupActivate;
        public AudioClip MissileActivate;

        void Update()
        {
            skillpush();
            if (GameController.GameResultPlay != 0f)
            {
                SkillClear();
            }

            if (ShieldActive != 0f)
            {
                SkillShield.SetActive(true);
            }
            else
            {
                SkillShield.SetActive(false);
            }
        }

        public void skillpush()
        {
            if (Input.GetKey(KeyCode.Alpha1) && Shield == 1)
            {
                ShieldActive = 1f;
                AudioSource.PlayClipAtPoint(ShieldActivate, transform.position);
                Debug.Log("释放护盾");
                Shield--;
            }

            //使用护盾道具
            if (Input.GetKey(KeyCode.Alpha2) && Speedup == 1)
            {
                Invoke("SpeedUp", 0.5f);
                AudioSource.PlayClipAtPoint(SpeedupActivate, transform.position);
                Movement.speed = 30;
                Debug.Log("释放加速");
                Speedup--;
            }

            //使用加速道具
            if (Input.GetKey(KeyCode.Space) && Missile == 1)
            {
                AudioSource.PlayClipAtPoint(MissileActivate, transform.position);
                Debug.Log("释放导弹");

            }
            //使用导弹道具
        }
        //玩家道具存储统计
        void SpeedUp()
        {
            Movement.speed = 6;
        }
        //加速效果
        void SkillClear()
        {
            Missile = 0;
            Speedup = 0;
            Shield = 0;
            ShieldActive = 0;
        }
        //游戏结束，清空玩家技能

    }
}

