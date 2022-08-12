using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework;

namespace RCToyCar
{
    public class EntityPlayerRCToyCar : Entity
    {

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            GameObject.FindWithTag("Canvas").transform.Find("Variable Joystick").GetComponent<VariableJoystick>();
        }

    }
}