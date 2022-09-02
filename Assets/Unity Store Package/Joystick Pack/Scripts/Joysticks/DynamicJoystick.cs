using GameFramework.Event;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RCToyCar
{
    public class DynamicJoystick : Joystick
    {
        public float MoveThreshold
        {
            get { return moveThreshold; }
            set { moveThreshold = Mathf.Abs(value); }
        }

        [SerializeField] private float moveThreshold = 1;

        protected override void Start()
        {
            MoveThreshold = moveThreshold;
            base.Start();
            background.gameObject.SetActive(false);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            background.gameObject.SetActive(false);
            base.OnPointerUp(eventData);
        }

        protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
        {
            if (magnitude > moveThreshold)
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
            GameEntry.Event.Fire(this, new CamControlEventArgs() {CamVertical =Vertical*0.4f,CamHorizontal = Horizontal*0.2f});


        }

        public void notMoving()
        {
            GameEntry.Event.Fire(this, new CamControlEventArgs() {CamHorizontal = 0,CamVertical =0});

        }
    }
}