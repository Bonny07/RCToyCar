using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public Rigidbody m_missile;  
    public Transform m_FireTransform;  
    public float m_LaunchForce = 30f;
    
    private string m_FireButton;
    private bool m_Fired;
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space)&&Skill.Missile==1)
        {
            Skill.Missile --;
            Fire();
        }
    }

    private void Fire()
    {
        Rigidbody shellInstance = Instantiate (m_missile, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        shellInstance.velocity = m_LaunchForce * m_FireTransform.forward; 
        Debug.Log("释放导弹");
    }
}
