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
    public CharacterController controller;
    public Transform cam;

    public static float speed = 6;
    Vector3 m_Velocity;
    float m_TurnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    private Rigidbody m_Rigidbody;
    private bool isCrashing;
    private float targetAngle;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        /*Cursor.lockState = CursorLockMode.Locked;*/
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

    void FixedUpdate()
    {
        /*Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(moveDir.normalized * (speed * Time.deltaTime));*/
        if (Input.GetKey(KeyCode.W) && isCrashing == false || Input.GetKey(KeyCode.A) && isCrashing == false ||
            Input.GetKey(KeyCode.S) && isCrashing == false || Input.GetKey(KeyCode.D) && isCrashing == false)
        {
            Vector3 movement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * speed * 0.01f;
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }

        if (isCrashing == true)
        {
            Vector3 movement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * speed * (-0.04f);
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isCrashing = true;
            Invoke("KnockBack", 0.8f);
        }
        if (collision.gameObject.CompareTag("Missile"))
        {
            Destroy(gameObject);
            Debug.Log("???");
        }
    }

    void KnockBack()
    {
        isCrashing = false;
    }
}