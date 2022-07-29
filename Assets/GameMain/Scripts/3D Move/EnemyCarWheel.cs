using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarWheel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject mygameobject1 = GameObject.Find("Emodel_LeftF");
        GameObject mygameobject2 = GameObject.Find("Emodel_LeftB");
        GameObject mygameobject3 = GameObject.Find("Emodel_RightF");
        GameObject mygameobject4 = GameObject.Find("Emodel_RightB");
        mygameobject1.transform.Rotate(Vector3.forward, 1 * Time.time);
        mygameobject2.transform.Rotate(Vector3.forward, 1 * Time.time);
        mygameobject3.transform.Rotate(Vector3.forward, 1 * Time.time);
        mygameobject4.transform.Rotate(Vector3.forward, 1 * Time.time);
    }
}
