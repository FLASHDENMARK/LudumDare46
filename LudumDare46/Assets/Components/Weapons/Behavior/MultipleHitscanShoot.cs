using Assets.Components.Weapons;
using Assets.Components.Weapons.Behavior;

public class MultipleHitscanShoot : HitscanShoot 
{
    public int Pellets = 5;
    public override void Execute (WeaponBase weapon)
    {
        int currentAmmoCount = weapon.Ammo;

        for (int i = 0; i < Pellets; i++)
        { 
            base.Execute(weapon);
        }

        // This is a hack :)
        weapon.Ammo = currentAmmoCount -= 1;
    }
}
