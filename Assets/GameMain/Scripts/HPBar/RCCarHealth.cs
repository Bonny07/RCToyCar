using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework;
using UnityGameFramework.Runtime;
using GameEntry = StarForce.GameEntry;

public class RCCarHealth : MonoBehaviour
{
    public float m_startHealth = 70f;
    public static float CurrentHealth;
    private bool m_Dead;   

    void Start()
    {
        CurrentHealth = m_startHealth;
    }

    void Update()
    {
        if (CurrentHealth <= 0f)
        {
            gameObject.SetActive (false);
            Destroy(gameObject);
            Debug.Log("GameOver");
            GameOver();
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            CurrentHealth -= 25f;
        }

    }

    public void Recover()
    {
        CurrentHealth += 20f;
    }

    public void GameOver()
    {
        /*ProcedureChangeScene(MenuForm);*/
    }
}
