using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissileExplosion : MonoBehaviour
{
    public float m_MaxLifeTime = 2f;  

    // Start is called before the first frame update
    void Start()
    {
        Destroy (gameObject, m_MaxLifeTime);
    }

    private void OnTriggerEnter (Collider other)
    {
        // 如果碰到敌人
        if (other.gameObject.tag.Equals("Enemy"))
        {
            EnemyHealth.CurrentHealth -= 100;
            other.GetComponent<EnemyHealth>().OnKilled();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        
    }
}
