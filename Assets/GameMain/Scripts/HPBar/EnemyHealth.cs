using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float m_startHealth = 70f;
    public float m_CurrentHealth;
    private bool m_Dead;
    private Rigidbody m_MissileBody;

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
            Debug.Log("You Win");
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_CurrentHealth -= 25f;
        }
    }

    public void OnKilled()
    {
        Destroy(this.transform.gameObject);
    }
    
    public void Recover()
    {
        m_CurrentHealth += 20f;
    }
}
