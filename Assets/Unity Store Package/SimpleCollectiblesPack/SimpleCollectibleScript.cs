using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour
{
    public enum CollectibleTypes
    {
        None,
        HealthRecover,  //修理包
        Shield,   //护盾
        SpeedUp,  //加速
        Missile,  //导弹
        Type5
    }; // you can replace this with your own labels for the types of collectibles in your game!

    public CollectibleTypes CollectibleType; // this gameObject's type

    public bool rotate; // do you want it to rotate?

    public float rotationSpeed;

    public AudioClip collectSound;

    public GameObject collectEffect;
    
    
    void Update()
    {
        if (rotate)
            transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime), Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")||other.CompareTag("Enemy"))
        {
            Collect();
        }
    }

    public void Collect()
    {
        //Below is space to add in your code for what happens based on the collectible type

        if (CollectibleType == CollectibleTypes.None)
        {
            //Add in code here;

            Debug.Log("Do NoType Command");
        }

        if (CollectibleType == CollectibleTypes.HealthRecover)
        {
            //TYPE 1  工具箱  回复20生命
            RCCarHealth.CurrentHealth += 20;
            collectitem();
            Destroy(gameObject);

            Debug.Log("拾取工具箱");
        }

        if (Skill.Shield == 0)
        {
            if (CollectibleType == CollectibleTypes.Shield)
            {
                //TYPE 2  护盾     使用时50%免伤2秒
                collectitem();

                Skill.Shield++;
                Destroy(gameObject);


                Debug.Log("拾取护盾");
            }
        }

        if (Skill.Speedup == 0)
        {
            if (CollectibleType == CollectibleTypes.SpeedUp)
            {
                //加速     使用时增加50%速度1秒
                
                collectitem();
                Skill.Speedup++;
                Destroy(gameObject);


                Debug.Log("拾取加速");
            }
        }

        if (Skill.Missile == 0)
        {
            if (CollectibleType == CollectibleTypes.Missile)
            {
                //使用时发射一颗子弹
                collectitem();
                Skill.Missile++;
                Destroy(gameObject);


                Debug.Log("拾取导弹");
            }
        }

        if (CollectibleType == CollectibleTypes.Type5)
        {
            //Add in code here;

            Debug.Log("Do NoType Command");
        }


    }

    public void collectitem()
    {
        if (collectSound)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        if (collectEffect)
            Instantiate(collectEffect, transform.position, Quaternion.identity);
    }

}

