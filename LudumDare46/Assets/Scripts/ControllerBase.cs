using Assets.Scripts.Managers;
using UnityEngine;

public abstract class ControllerBase : MonoBehaviour, IDamageReceiver, IDamageGiver {
    public float health;
    public float Health { get => health; }

    public bool IsDead { get; protected set; }

    public ControllerBase DamageGiver => this;

    public string causeOfDamage;
    public string CauseOfDamage { get => causeOfDamage; }

    protected TriggerBase _trigger;

    public void ReceiveDamage (float damage, IDamageGiver damageGiver)
    {
        health -= damage;

        if (health <= 0)
        {
            if (!IsDead)
            {
                Die(damageGiver);
            }

            IsDead = true;
        }
    }

    public void OnPlayerInsideInteractionTrigger(TriggerBase inter)
    {
        _trigger = inter;
    }

    public void OnPlayerOutsideInteractionTrigger()
    {
        _trigger = null;
    }

    public abstract void Die (IDamageGiver damageGiver);

    public void Alert(ControllerBase damageGiver)
    {
        // You cannot alert yourself
        if (damageGiver != this && !IsDead)
        {
            HUD.DisplaySubtitles("An NPC was alerted", "FAIL", 1F);
        }
    }
}