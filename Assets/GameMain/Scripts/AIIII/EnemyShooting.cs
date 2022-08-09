using System.Collections;
using System.Collections.Generic;
using RCToyCar;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Rigidbody m_missile;
    public Transform m_FireTransform;

    private string m_FireButton;
    private bool m_Fired;

    void Update()
    {
        if (EnemySkill.EnemyShoot == 1)
        {
            EnemySkill.EnemyShoot--;
            Fire();
            Debug.Log("敌人释放导弹");
        }
    }
    
    private void Fire()
    {
        Rigidbody shellInstance = Instantiate(m_missile, m_FireTransform.position, m_FireTransform.rotation);
        shellInstance.velocity = PlayerSkill.MissileFlySpeed * m_FireTransform.forward;
    }
    //使用导弹道具后，从敌人小车发射点向前发射一枚导弹
}
