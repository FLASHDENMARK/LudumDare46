using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Managers
{
    public class GameplayManager : MonoBehaviour
    {
        public bool noFailMode = false;
        private static PlayerController player;
        public delegate void OnControllerKilled (ControllerBase controller, IDamageGiver attacker);
        public delegate void OnFailed (string reason, string tip = null);
        private string tempSpeaker = "GAME";
        public static OnControllerKilled OnControllerKilledEvent;
        public static OnFailed OnFailedEvent;

        [Header("InGame Time"), SerializeField]
        public float TimeScale;
        public float StartHours = 12;
        public static IngameTime GameTime = new IngameTime();

        [SerializeField]
        IngameTime time;

        public GameObject notesGameObject;
        private Notes notes;

        private void OnEnable ()
        {
            // Deterministic randomness for the win!
            UnityEngine.Random.InitState(0);

            notes = notesGameObject.GetComponent<Notes>();

            OnControllerKilledEvent += OnHandleControllerKilled;
            OnFailedEvent += OnHandledFailedEvent;
        }

        private void OnDisable ()
        {
            OnControllerKilledEvent -= OnHandleControllerKilled;
            OnFailedEvent -= OnHandledFailedEvent;
        }

        private void OnHandledFailedEvent(string reason, string tip)
        {
            string failedTime = GameTime.ToString();
            string text = $"You failed your target at '{failedTime}'. {reason}";

            if (!string.IsNullOrEmpty(tip))
            {
                text += " Tip: " + tip;
            }

            if (!noFailMode)
            {
                StartCoroutine(Fail(3, text));
            }
        }

        IEnumerator Fail(float waitTime, string failText = "You failed")
        {
            float timeScale = Time.timeScale;

            HUD.DisplayFailedScreen(failText);

            //Time.timeScale = 0.0F;
            yield return new WaitForSeconds(waitTime);
            Time.timeScale = timeScale;

            SceneManager.LoadScene(0);
        }

        [Serializable]
        public class IngameTime
        {
            public int hour, minute, second;

            public static string Convert (int toConvert)
            {
                string result;

                if (toConvert < 10)
                {
                    result = "0" + toConvert;
                }
                else if (toConvert == 0)
                {
                    result = "00";
                }
                else
                {
                    result = toConvert.ToString();
                }

                return result;
            }

            public override string ToString()
            {
                string h = Convert(hour);
                string m = Convert(minute);
                string s = Convert(second);

                return $"{h}:{m}:{s}";
            }
        }

        public static PlayerController GetPlayer()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            }

            return player;
        }

        bool _displayControls = false;

        private void Update()
        {
            UpdateIngameTime();
            GameTime.hour = time.hour;
            GameTime.minute = time.minute;
            GameTime.second = time.second;

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                _displayControls = !_displayControls;
            }

            //HUD.DisplayControls(_displayControls);
        }


        private void UpdateIngameTime()
        {
            float currentTime = Time.time * TimeScale + (StartHours * 3600);
            time.hour = Mathf.FloorToInt(currentTime / 3600);
            time.minute = Mathf.FloorToInt((currentTime % 3600) / 60);
            time.second = Mathf.FloorToInt((currentTime % 3600) % 60);
        }

        void OnHandleControllerKilled (ControllerBase controller, IDamageGiver attacker)
        {
            if (controller is PlayerController)
            {
                // The player died. Reset
                OnHandledFailedEvent("You died. You know that is not meant to happen... We never tested that during development...", "Dont die next time :)");
            }

            else if (controller is HitmanController)
            {
                HUD.DisplaySubtitles(tempSpeaker, "A hitman has been killed. Good work.", 5.0F);
            }
            else if (controller is AIController)
            {
                // A civilian died.

                // The target has died
                if ((controller as AIController).isTarget)
                {
                    OnHandledFailedEvent("You failed to protect the target", "Make sure to keep it alive");
                }

                // The player has killed a civilian
                if (attacker.DamageGiver is PlayerController)
                {
                    int i = UnityEngine.Random.Range(0, 3);

                    string fail = "";

                    switch (i)
                    {
                        case 0:
                            fail = "Do not kill civilians.";
                            break;
                        case 1:
                            fail = "You swore to protect the civilians.";
                            break;
                        case 2:
                            fail = "You will make sure no harm comes to the civilians.";
                            break;
                    }

                    OnHandledFailedEvent(fail, null);

                    // The target has died
                    if ((controller as AIController).isTarget)
                    {
                        OnHandledFailedEvent("You killed the target you were meant to protect.", null);
                    }
                }
                else if (attacker.DamageGiver is HitmanController)
                {
                    HUD.DisplaySubtitles(tempSpeaker, "A hitman has killed a target. Do not let them get away with that.", 5.0F);
                    string cause = attacker.DamageGiver.CauseOfDamage;

                    if (cause == null)
                    {
                        cause = "This needs to be fixed before release...";
                    }

                    notes.TargetDied(GameTime, cause);
                    OnHandledFailedEvent("A hitman has killed a target. Do not let them get away with that next time", "");
                }
                else if (attacker.CauseOfDamage == "Poison") {
                    notes.TargetDied(GameTime, attacker.CauseOfDamage);
                    OnHandledFailedEvent("A hitman has killed a target. Do not let them get away with that next time", "");
                }
            }
        }
    }
}
