using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameplayManager : MonoBehaviour
    {
        public delegate void OnControllerKilled (ControllerBase controller);

        public static OnControllerKilled OnControllerKilledEvent;

        [Header("InGame Time"), SerializeField]
        public float TimeScale;
        public float StartHours = 12;
        public static IngameTime GameTime = new IngameTime();

        [SerializeField]
        IngameTime time;
        


        private void OnEnable ()
        {
            OnControllerKilledEvent += OnHandleControllerKilled;
        }

        private void OnDisable ()
        {
            OnControllerKilledEvent += OnHandleControllerKilled;
        }

        [Serializable]
        public class IngameTime
        {
            public int hour, minute, second;
        }

        private void Update()
        {
            UpdateIngameTime();
            time.hour = GameTime.hour;
            time.minute = GameTime.minute;
            time.second = GameTime.second;
        }


        private void UpdateIngameTime()
        {
            float currentTime = Time.time * TimeScale + (StartHours * 3600);
            GameTime.hour = Mathf.FloorToInt(currentTime / 3600);
            GameTime.minute = Mathf.FloorToInt((currentTime % 3600) / 60);
            GameTime.second = Mathf.FloorToInt((currentTime % 3600) % 60);
        }

        void OnHandleControllerKilled (ControllerBase controller)
        {
            if (controller is PlayerController)
            {
                // The player died. Reset
            }
            else if (controller is CivilianController)
            {
                // A civilian died.
            }
            else if (controller is HitmanController)
            {

            }
        }
    }
}
