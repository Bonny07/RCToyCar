using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillspawner : MonoBehaviour
{
    public GameObject skillobject;
    public float coldDown = 2f;
    public float nextSpawn;


    void Start()
    {
        
    }
    
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + coldDown;
            Instantiate(skillobject, transform);
        }
    }
}
