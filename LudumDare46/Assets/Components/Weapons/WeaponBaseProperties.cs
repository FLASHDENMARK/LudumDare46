using Assets.Components.Weapons.Behavior;
using Assets.Components.Weapons.Effects;
using System;
using UnityEngine;

namespace Assets.Components.Weapons
{
    public partial class WeaponBase
    {
        public int Ammo = 32;
        public int SpareAmmo = 96;

        public int MaxAmmo = 32;
        public int MaxSpareAmmo = 192;
        public float ShootTime = 0.11F;

        public float ReloadTime = 1.25F;

        public Transform Camera;

        public readonly float EquipTime = 0.65F;
        public readonly float UnequipTime = 0.35F;

        public bool IsReloading => WeaponBehavior.Reload.IsExecuting;
        public bool CanReload => !IsReloading && Ammo < MaxAmmo && SpareAmmo > 0;
        public bool CanShoot => !IsReloading && !WeaponBehavior.Shoot.IsExecuting && Ammo > 0 && Time.time > LastShootTime + ShootTime;

        public float LastShootTime { get; internal set; }

        public Sound WeaponSound;
        public Animation WeaponAnimation;
        public Behavior WeaponBehavior;
        internal bool isEquipped;

        [Serializable]
        public struct Sound
        {
            public AudioSource Source;

            public AudioClip[] Shoot;
            public AudioClip[] Reload;
            public AudioClip[] Equip;
            public AudioClip[] Unequip;
            public AudioClip[] Zoom;

            public AudioClip[] Empty { get; internal set; }
        }

        [Serializable]
        public struct Animation
        {
            public AnimationClip Shoot;
            public AnimationClip Reload;
            public AnimationClip Equip;
            public AnimationClip Unequip;
            public AnimationClip Zoom;
        }

        [Serializable]
        public class Behavior
        {
            public WeaponBehaviorBase Shoot;
            public WeaponBehaviorBase Reload;
            public WeaponBehaviorBase Equip;
            public WeaponBehaviorBase Unequip;

            public WeaponEffectsBase ShootEffect;
            
            public bool IsExecuting => Shoot.IsExecuting || Reload.IsExecuting || Equip.IsExecuting || Unequip.IsExecuting;
        }

        [Serializable]
        public class HUD
        {
            public Sprite Weapon;
            public Sprite Crosshair;
            public Sprite Ammo;
        }
    }
}
