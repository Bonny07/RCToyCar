using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RCToyCar
{
    public class CarWheel : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||
                Input.GetKey(KeyCode.S))
            {
                GameObject mygameobject1 = GameObject.Find("model_LeftF");
                GameObject mygameobject2 = GameObject.Find("model_LeftB");
                GameObject mygameobject3 = GameObject.Find("model_RightF");
                GameObject mygameobject4 = GameObject.Find("model_RightB");
                mygameobject1.transform.Rotate(Vector3.forward, 1 * Time.time);
                mygameobject2.transform.Rotate(Vector3.forward, 1 * Time.time);
                mygameobject3.transform.Rotate(Vector3.forward, 1 * Time.time);
                mygameobject4.transform.Rotate(Vector3.forward, 1 * Time.time);
            }
        }

    }
}