using System.Collections;
using System.Collections.Generic;
using StarForce;
using UnityEngine;

public class RCCarHealth : MonoBehaviour
{
    public float m_startHealth = 70f;
    public float m_CurrentHealth;
    private bool m_Dead;   

    void Start()
    {
        m_CurrentHealth = m_startHealth;
    }

    void Update()
    {
        if (m_CurrentHealth <= 0f)
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
            m_CurrentHealth -= 25f;
        }
    }

    public void Recover()
    {
        m_CurrentHealth += 20f;
    }

    public void GameOver()
    {
        /*ProcedureChangeScene(MenuForm);*/
    }
}
