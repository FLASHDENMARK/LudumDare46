using UnityEngine;

namespace Assets.Components.Weapons.Behavior
{
    public abstract class WeaponBehaviorBase : MonoBehaviour
    {
        public abstract bool IsExecuting { get; set; }
        public abstract void Execute (WeaponBase weapon);
    }
}