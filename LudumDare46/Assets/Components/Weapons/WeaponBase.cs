using System.Collections;
using UnityEngine;

namespace Assets.Components.Weapons
{
    public partial class WeaponBase : MonoBehaviour
    {
        public void Equip ()
        {
            WeaponBehavior.Equip.Execute(this);
        }

        public void Unequip ()
        {
            WeaponBehavior.Unequip.Execute(this);
        }

        public void Reload ()
        {
            if (CanReload)
            {
                WeaponBehavior.Reload.Execute(this);
            }
        }

        public void ReloadIfEmpty ()
        {
            if (Ammo <= 0)
            {
                Reload();
            }
        }

        public void Zoom ()
        {

        }

        public void Shoot ()
        {
            if (CanShoot)
            {
                WeaponBehavior.Shoot.Execute(this);

                if (WeaponBehavior.ShootEffect != null)
                {
                    WeaponBehavior.ShootEffect.Execute(this);
                }
            }
            else if (Ammo == 0 && !IsReloading)
            {
                Play(WeaponSound.Empty);
            }

            if (Ammo == 0)
            {
                Reload();
            }
        }

        internal void Play (AudioClip[] sounds)
        {
            AudioClip clip = GetSound(sounds);

            if (clip == null)
            {
                return;
            }

            WeaponSound.Source.PlayOneShot(clip);
        }

        internal void Play (AnimationClip animation)
        {
            // TODO
        }

        private AudioClip GetSound (AudioClip[] sounds)
        {
            if (sounds == null || sounds.Length == 0)
            {
                return null; 
            }

            int index = Random.Range(0, sounds.Length);
            
            return sounds[index];
        }
    }
}