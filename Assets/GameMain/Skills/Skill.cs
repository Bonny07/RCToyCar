using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public static float Shield = 0;
    public static float Speedup = 0;
    public static float Missile = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        skillpush();
    }

    public void skillpush()
    {
        if (Input.GetKey(KeyCode.Alpha1)&&Shield==1)
        {
            
            
            Debug.Log("释放护盾");
            Shield--;
        }
        if (Input.GetKey(KeyCode.Alpha2)&&Speedup==1)
        {
            Invoke("SpeedUp", 0.5f);
            Movement.speed = 36;
            Debug.Log("释放加速");
            Speedup--;
        }
        if (Input.GetKey(KeyCode.Space)&&Missile==1)
        {

            Debug.Log("释放导弹");
            
        }
    }
    void SpeedUp()
    {
        Movement.speed = 6;
    }    
}


