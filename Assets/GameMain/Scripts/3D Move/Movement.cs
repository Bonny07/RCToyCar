using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityGameFramework.Runtime;

public class Movement : MonoBehaviour
{
    public Transform cam;

    public static float speed = 12;  //玩家移动速度倍率
    Vector3 m_Velocity;
    float m_TurnSmoothVelocity;
    public float turnSmoothTime = 0.1f;  //玩家模型转向顺滑度
    private Rigidbody m_Rigidbody;
    private bool isCrashing;  //玩家是否处于被撞后的眩晕状态
    private float targetAngle;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_TurnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }  
    //玩家人物移动，镜头控制人物移动方向

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) && isCrashing == false || Input.GetKey(KeyCode.A) && isCrashing == false ||
            Input.GetKey(KeyCode.S) && isCrashing == false || Input.GetKey(KeyCode.D) && isCrashing == false)
        {
            Vector3 movement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * speed * 0.01f;
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }  
        //玩家移动

        if (isCrashing == true)
        {
            Vector3 movement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * speed * (-0.04f);
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }  
        //玩家碰撞敌方后被击退
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isCrashing = true;
            Invoke("KnockBack", 0.8f);
        }  
        //玩家碰撞敌方后被击退
        if (collision.gameObject.CompareTag("Missile"))
        {
            RCCarHealth.CurrentHealth = 0;
            Debug.Log("???");
        }  
        //玩家被导弹击中后血量归零
    }

    void KnockBack()
    {
        isCrashing = false;
    }  
}