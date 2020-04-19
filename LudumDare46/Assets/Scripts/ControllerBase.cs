using Assets.Scripts.Managers;
using UnityEngine;

public abstract class ControllerBase : MonoBehaviour, IDamageReceiver
{
    public float health;
    public float Health { get => health; }

    public bool IsDead { get; private set; }

    public void ReceiveDamage (float damage, IDamageGiver damageGiver)
    {
        health -= damage;

        if (health <= 0)
        {
            if (!IsDead)
            {
                GameplayManager.OnControllerKilledEvent(this);

                Die(damageGiver);
            }

            IsDead = true;
        }
    }

    public abstract void Die (IDamageGiver damageGiver);
}