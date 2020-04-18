using Assets.Components.Weapons;
using Assets.Components.Weapons.Behavior;
using Assets.Scripts;
using UnityEngine;

public class ProjectileShoot : WeaponBehaviorBase
{
    public override bool IsExecuting { get; set; }

    public GameObject Projectile;
    public GameObject SpawnPoint;

    public Vector3 maxRandomness;

    public Transform shooterTest;

    private Vector3 _spawnPointPosition;

    private void Awake ()
    {
        _spawnPointPosition = SpawnPoint.transform.localPosition;
    }

    public override void Execute (WeaponBase weapon)
    {
        weapon.Play(weapon.WeaponSound.Shoot);

        weapon.LastShootTime = Time.time;

        weapon.Ammo--;

        //ProjectileSpawnParameters spawnParameters = new ProjectileSpawnParameters(shooterTest);

        Vector3 randomness = new Vector3(Random.Range(-maxRandomness.x, maxRandomness.x), Random.Range(-maxRandomness.y, maxRandomness.y), Random.Range(-maxRandomness.z, maxRandomness.z));

        SpawnPoint.transform.localPosition = _spawnPointPosition + randomness;

        //Manager.Spawn(Projectile, SpawnPoint, spawnParameters);
    }
}