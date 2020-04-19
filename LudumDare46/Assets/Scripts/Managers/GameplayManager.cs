using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameplayManager : MonoBehaviour
    {
        public string tempSpeaker = "GAME";
        public delegate void OnControllerKilled(ControllerBase controller, IDamageGiver attacker);
        private bool _displayControls = false;

        public List<ObjectiveTimeline> timelines = new List<ObjectiveTimeline>(); 

        public static OnControllerKilled OnControllerKilledEvent;

        private void OnEnable ()
        {
            OnControllerKilledEvent += OnHandleControllerKilled;
        }

        private void OnDisable ()
        {
            OnControllerKilledEvent += OnHandleControllerKilled;
        }

        private void Awake()
        {
            HUD.DisplaySubtitles(tempSpeaker, "Welcome to 'Target in danger. Keep It Alive at all cost.'", 5F);

            //HUD.DisplaySubtitles(tempSpeaker, "This is a test", 5F);
        }
    
        void Update ()
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                _displayControls = !_displayControls;
            }

            HUD.DisplayControls(_displayControls);
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
                    int i = Random.Range(0, 3);
                    switch (i)
                    {
                        case 1:
                            HUD.DisplaySubtitles(tempSpeaker, "Do not kill civilians.", 5.0F);
                            break;
                        case 2:
                            HUD.DisplaySubtitles(tempSpeaker, "You will be punished for killing civilians.", 5.0F);
                            break;
                        case 3:
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
