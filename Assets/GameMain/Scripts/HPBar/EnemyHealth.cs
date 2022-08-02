using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float m_startHealth = 70f;
    public static float CurrentHealth;
    private bool m_Dead;
    private Rigidbody m_MissileBody;

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

        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CurrentHealth -= 25f;
        }
        if (collision.gameObject.CompareTag("Missile"))
        {
            CurrentHealth -= 200f;
        }
    }

    public void OnKilled()
    {
        Destroy(this.transform.gameObject);
    }
    
    public void Recover()
    {
        CurrentHealth += 20f;
    }
}
