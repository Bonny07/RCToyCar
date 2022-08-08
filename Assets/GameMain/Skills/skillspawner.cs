using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCToyCar
{
    public class skillspawner : MonoBehaviour
    {
        public GameObject skillobject1;
        public GameObject skillobject2;
        public GameObject skillobject3;

        public float coldDown = 2f;
        public float nextSpawn = 1f;
        public int r;

        private void Start()
        {
            
        }

        void Update()
        {
            r = UnityEngine.Random.Range(1, 4);
            if (r == 1)
            {
                if (Time.time > nextSpawn)
                {
                    nextSpawn = Time.time + coldDown;
                    Instantiate(skillobject1, transform);
                    r = UnityEngine.Random.Range(0, 4);
                }
            }

            if (r == 2)
            {
                if (Time.time > nextSpawn)
                {
                    nextSpawn = Time.time + coldDown;
                    Instantiate(skillobject2, transform);
                    r = UnityEngine.Random.Range(0, 4);
                }
            }

            if (r == 3)
            {
                if (Time.time > nextSpawn)
                {
                    nextSpawn = Time.time + coldDown;
                    Instantiate(skillobject3, transform);
                    r = UnityEngine.Random.Range(0, 4);
                }
            }
        }


    }
}
