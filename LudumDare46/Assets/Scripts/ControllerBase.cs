using Assets.Scripts.Managers;
using UnityEngine;

public abstract class ControllerBase : MonoBehaviour, IDamageReceiver
{
    public float health;
    public float Health { get => health; }

    public bool IsDead { get; private set; }
    public TriggerBase trigger;

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

    public void OnPlayerInsideInteractionTrigger(TriggerBase inter)
    {
        trigger = inter;
    }

    public void OnPlayerOutsideInteractionTrigger()
    {
        trigger = null;
    }

    public abstract void Die (IDamageGiver damageGiver);
}