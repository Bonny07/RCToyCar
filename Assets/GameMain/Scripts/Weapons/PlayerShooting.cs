using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.DataTable;

namespace RCToyCar
{
    
        public class PlayerShooting : MonoBehaviour
        {
            public int m_PlayerNumber = 1;
            public Rigidbody m_missile;
            public Transform m_FireTransform;

            private string m_FireButton;
            private bool m_Fired;

            
            void Update()
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    MissileShoot();
                }
                //使用导弹
            }

            public void MissileShoot()
            {
                if (PlayerSkill.Missile == 1)
                {
                    PlayerSkill.Missile--;
                    Fire();
                }
            }

            private void Fire()
            {
                Rigidbody shellInstance = Instantiate(m_missile, m_FireTransform.position, m_FireTransform.rotation);
                shellInstance.velocity = PlayerSkill.MissileFlySpeed * m_FireTransform.forward;
                Debug.Log("释放导弹");
            }
            //使用导弹道具后，从小车发射点向前发射一枚导弹
            
        }
    }
