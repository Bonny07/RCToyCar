using UnityEngine;
using GameFramework.DataTable;

namespace RCToyCar
{
    public class SimpleCollectibleScript : MonoBehaviour
    {
        public enum CollectibleTypes
        {
            None,
            HealthRecover, //修理包
            Shield, //护盾
            SpeedUp, //加速
            Missile, //导弹
            Type5
        };

        public CollectibleTypes CollectibleType;  //道具属性
        public bool rotate;  //是否旋转
        public float rotationSpeed;  //旋转速度
        public AudioClip collectSound;  //播放拾取道具声音
        public GameObject collectEffect;  //获取道具效果

        private int PlayerMaxPropStorage;
        private int EnemyMaxPropStorage;

        private void Start()
        {
            PropOnLoad();
        }


        void Update()
        {
            if (rotate)
            {
                transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime), Space.World);
            }

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerCollect();
            }
            if (other.CompareTag("Enemy"))
            {
                EnemyCollect();
            }
        }

        public void PlayerCollect()
        {
            if (collectSound)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
            //Below is space to add in your code for what happens based on the collectible type

            if (CollectibleType == CollectibleTypes.None)
            {
                //Add in code here;

                Debug.Log("Do NoType Command");
            }

            if (CollectibleType == CollectibleTypes.HealthRecover)
            {
                //TYPE 1  工具箱  回复20生命

                RCCarHealth.CurrentHealth += PlayerSkill.HealingHP;
                collectitem();
                Destroy(gameObject);
                Debug.Log("玩家拾取工具箱");
            }

            if (PlayerSkill.Shield < PlayerMaxPropStorage)
            {
                if (CollectibleType == CollectibleTypes.Shield)
                {
                    //TYPE 2  护盾     使用时50%免伤2秒
                    collectitem();
                    PlayerSkill.Shield++;
                    Destroy(gameObject);


                    Debug.Log("玩家拾取护盾");
                }
            }

            if (PlayerSkill.Speedup < PlayerMaxPropStorage)
            {
                if (CollectibleType == CollectibleTypes.SpeedUp)
                {
                    //加速     使用时增加50%速度1秒

                    collectitem();
                    PlayerSkill.Speedup++;
                    Destroy(gameObject);


                    Debug.Log("玩家拾取加速");
                }
            }

            if (PlayerSkill.Missile < PlayerMaxPropStorage)
            {
                if (CollectibleType == CollectibleTypes.Missile)
                {
                    //使用时发射一颗子弹
                    collectitem();
                    PlayerSkill.Missile++;
                    Destroy(gameObject);

                    Debug.Log("玩家拾取导弹");
                }
            }

            if (CollectibleType == CollectibleTypes.Type5)
            {
                //Add in code here;
                Debug.Log("Do NoType Command");
            }
        }
        
        public void EnemyCollect()
        {
            if (collectSound)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            if (CollectibleType == CollectibleTypes.None)
            {
                //Add in code here;

                Debug.Log("Do NoType Command");
            }

            if (CollectibleType == CollectibleTypes.HealthRecover)
            {
                //TYPE 1  工具箱  回复20生命

                EnemyHealth.CurrentHealth += PlayerSkill.HealingHP;
                collectitem();
                Destroy(gameObject);
                Debug.Log("敌人拾取工具箱");
            }

            if (EnemySkill.Shield < EnemyMaxPropStorage)
            {
                if (CollectibleType == CollectibleTypes.Shield)
                {
                    //TYPE 2  护盾     使用时50%免伤2秒
                    EnemySkill.skillnum = 1;
                    collectitem();
                    EnemySkill.Shield++;
                    Destroy(gameObject);
                    Debug.Log("敌人拾取护盾");
                }
            }

            if (EnemySkill.Speedup < EnemyMaxPropStorage)
            {
                if (CollectibleType == CollectibleTypes.SpeedUp)
                {
                    //加速     使用时增加50%速度1秒
                    EnemySkill.skillnum = 1;
                    collectitem();
                    EnemySkill.Speedup++;
                    Destroy(gameObject);
                    
                    Debug.Log("敌人拾取加速");
                }
            }

            if (EnemySkill.Missile < EnemyMaxPropStorage)
            {
                if (CollectibleType == CollectibleTypes.Missile)
                {
                    //敌人发射一颗子弹
                    EnemySkill.skillnum = 1;
                    collectitem();
                    EnemySkill.Missile++;
                    Destroy(gameObject);
                    Debug.Log("敌人拾取导弹");
                }
            }

            if (CollectibleType == CollectibleTypes.Type5)
            {
                //Add in code here;
                Debug.Log("Do NoType Command");
            }
        }

        public void collectitem()
        {
            if (collectSound)
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            if (collectEffect)
                Instantiate(collectEffect, transform.position, Quaternion.identity);
        }
        
        void PropOnLoad()
        {
            IDataTable<DRRCToyCar> dtProperties = GameEntry.DataTable.GetDataTable<DRRCToyCar>();
            DRRCToyCar drPlayerStorage = dtProperties.GetDataRow(10000);
            DRRCToyCar drEnemyStorage = dtProperties.GetDataRow(10001);
            if (drPlayerStorage == null || drEnemyStorage == null)
            {
                return;
            }

            PlayerMaxPropStorage = drPlayerStorage.PropStorage;
            EnemyMaxPropStorage = drEnemyStorage.PropStorage;
        }
        //道具数据加载
    }

}