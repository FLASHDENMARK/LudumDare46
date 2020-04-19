﻿using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameplayManager : MonoBehaviour
    {
        public delegate void OnControllerKilled (ControllerBase controller, IDamageGiver attacker);
        private string tempSpeaker = "GAME";
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

        bool _displayControls = false;

        private void Update()
        {
            UpdateIngameTime();
            time.hour = GameTime.hour;
            time.minute = GameTime.minute;
            time.second = GameTime.second;

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                _displayControls = !_displayControls;
            }

            //HUD.DisplayControls(_displayControls);
        }


        private void UpdateIngameTime()
        {
            float currentTime = Time.time * TimeScale + (StartHours * 3600);
            GameTime.hour = Mathf.FloorToInt(currentTime / 3600);
            GameTime.minute = Mathf.FloorToInt((currentTime % 3600) / 60);
            GameTime.second = Mathf.FloorToInt((currentTime % 3600) % 60);
        }

        void OnHandleControllerKilled (ControllerBase controller, IDamageGiver attacker)
        {
            if (controller is PlayerController)
            {
                // The player died. Reset
                HUD.DisplaySubtitles(tempSpeaker, "You died.", 5.0F);
            }
            else if (controller is CivilianController)
            {
                // A civilian died.

                // The player has killed a civilian
                if (attacker.DamageGiver is PlayerController)
                {
                    int i = UnityEngine.Random.Range(0, 3);

                    switch (i)
                    {
                        case 0:
                            HUD.DisplaySubtitles(tempSpeaker, "Do not kill civilians.", 5.0F);
                            break;
                        case 1:
                            HUD.DisplaySubtitles(tempSpeaker, "You will be punished for killing civilians.", 5.0F);
                            break;
                        case 2:
                            HUD.DisplaySubtitles(tempSpeaker, "Bastian. Hold op med det.", 5.0F);
                            break;

                    }
                }
                else if (attacker is HitmanController)
                {
                    HUD.DisplaySubtitles(tempSpeaker, "A hitman has killed a target. Do not let them get away with that.", 5.0F);
                }
            }
            else if (controller is HitmanController)
            {
                HUD.DisplaySubtitles(tempSpeaker, "A hitman has been killed. Good work.", 5.0F);
            }
        }
    }
}