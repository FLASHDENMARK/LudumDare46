using Assets.Components.Weapons;
using UnityEngine;

public enum WeaponTypeEnum { Knife, Pistol }

public class PickupableToWeapon
{
    public WeaponTypeEnum weaponType;
    public WeaponBase weapon;
}
public class Pickupable : MonoBehaviour
{
    public WeaponTypeEnum weapon;
}