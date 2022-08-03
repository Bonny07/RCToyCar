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
    
    public GameObject m_ExplosionPrefab;       
    private ParticleSystem m_ExplosionParticles;


    private void Awake()
    {
        m_ExplosionParticles = Instantiate (m_ExplosionPrefab).GetComponent<ParticleSystem> ();
        m_ExplosionParticles.gameObject.SetActive (false);
    }

    void Start()
    {
        CurrentHealth = m_startHealth;
    }

    void Update()
    {
        if (CurrentHealth <= 0f)
        {
            OnDeath();
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            CurrentHealth -= 25f;
        }
        if (collision.gameObject.CompareTag("Missile"))
        {
            CurrentHealth -= 200f;
        }
    }

    public void Recover()
    {
        CurrentHealth += 20f;
    }

    private void OnDeath()
    {
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive (true);
        m_ExplosionParticles.Play ();
        gameObject.SetActive (false);
    }
}
