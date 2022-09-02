using UnityEngine;


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

        public CollectibleTypes CollectibleType; //道具属性
        public bool rotate; //是否旋转
        public float rotationSpeed; //旋转速度
        public GameObject collectEffect; //获取道具效果

        private int PlayerMaxPropStorage;
        private int EnemyMaxPropStorage;

        private void Start()
        {
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
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                Collect();
            }
        }

        public void Collect()
        {
            if (CollectibleType == CollectibleTypes.None)
            {
                //Add in code here;

                Debug.Log("Do NoType Command");
            }

            if (CollectibleType == CollectibleTypes.HealthRecover)
            {
                //TYPE 1  工具箱  回复20生命
                Destroying();
                Destroy(gameObject);
                Debug.Log("拾取治疗");
            }


            if (CollectibleType == CollectibleTypes.Shield)
            {
                //TYPE 2  护盾     使用时50%免伤2秒
                Destroying();
                Debug.Log("拾取护盾");
            }


            if (CollectibleType == CollectibleTypes.SpeedUp)
            {
                //加速     使用时增加50%速度1秒
                Destroying();
                Debug.Log("拾取加速");
            }


            if (CollectibleType == CollectibleTypes.Missile)
            {
                //使用时发射一颗子弹
                Destroying();
                Debug.Log("拾取导弹");
            }


            if (CollectibleType == CollectibleTypes.Type5)
            {
                //Add in code here;
                Debug.Log("Do NoType Command");
            }
        }

        private void Destroying()
        {
            collectitem();
            Destroy(gameObject);
        }

        public void collectitem()
        {
            if (collectEffect)
                Instantiate(collectEffect, transform.position, Quaternion.identity);
        }

    }
}