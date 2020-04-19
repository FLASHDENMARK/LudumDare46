using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameplayManager : MonoBehaviour
    {
        public delegate void OnControllerKilled (ControllerBase controller);

        public static OnControllerKilled OnControllerKilledEvent;

        private void OnEnable ()
        {
            OnControllerKilledEvent += OnHandleControllerKilled;
        }

        private void OnDisable ()
        {
            OnControllerKilledEvent += OnHandleControllerKilled;
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
