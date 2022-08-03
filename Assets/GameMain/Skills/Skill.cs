using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public static float Shield = 0;
    public static float Speedup = 0;
    public static float Missile = 0;

    void Update()
    {
        skillpush();
    }

    public void skillpush()
    {
        if (Input.GetKey(KeyCode.Alpha1)&&Shield==1)
        {
            RCCarHealth.CurrentHealth += 20;
            Debug.Log("释放护盾");
            Shield--;
        }
        //使用护盾道具
        if (Input.GetKey(KeyCode.Alpha2)&&Speedup==1)
        {
            Invoke("SpeedUp", 0.5f);
            Movement.speed = 36;
            Debug.Log("释放加速");
            Speedup--;
        }
        //使用加速道具
        if (Input.GetKey(KeyCode.Space)&&Missile==1)
        {

            Debug.Log("释放导弹");
            
        }
        //使用导弹道具
    }
    //玩家道具存储统计
    void SpeedUp()
    {
        Movement.speed = 6;
    }    
}


