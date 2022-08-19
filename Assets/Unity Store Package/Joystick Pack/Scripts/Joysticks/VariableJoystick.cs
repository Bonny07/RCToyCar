using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RCToyCar
{

    public class VariableJoystick : Joystick
    {
        public float MoveThreshold
        {
            get { return moveThreshold; }
            set { moveThreshold = Mathf.Abs(value); }
        }

        [SerializeField] private float moveThreshold = 1;
        [SerializeField] private JoystickType joystickType = JoystickType.Fixed;

        private Vector2 fixedPosition = Vector2.zero;

        public void SetMode(JoystickType joystickType)
        {
            this.joystickType = joystickType;
            if (joystickType == JoystickType.Fixed)
            {
                background.anchoredPosition = fixedPosition;
                background.gameObject.SetActive(true);
            }
            else
                background.gameObject.SetActive(false);
        }

        protected override void Start()
        {
            base.Start();
            fixedPosition = background.anchoredPosition;
            SetMode(joystickType);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (joystickType != JoystickType.Fixed)
            {
                background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
                background.gameObject.SetActive(true);
            }

            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (joystickType != JoystickType.Fixed)
                background.gameObject.SetActive(false);

            base.OnPointerUp(eventData);
        }

        protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
        {
            if (joystickType == JoystickType.Dynamic && magnitude > moveThreshold)
            {
                Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
                background.anchoredPosition += difference;
            }

            base.HandleInput(magnitude, normalised, radius, cam);
        }

        private void Update()
        {
            if (Horizontal != 0 || Vertical != 0)
            {
                Moving();
            }
            else
            {
                notMoving();
            }
        }

        public void Moving()
        {
            GameEntry.Event.Fire(this,new JoyStickEventArgs() { Vertical= Vertical,Horizontal=Horizontal });

        }

        public void notMoving()
        {
            GameEntry.Event.Fire(this,new JoyStickEventArgs() { Vertical= 0,Horizontal=0 });
            
        }
    }
}


public enum JoystickType { Fixed, Floating, Dynamic }