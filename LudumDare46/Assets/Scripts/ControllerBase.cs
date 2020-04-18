using UnityEngine;

public abstract class ControllerBase : MonoBehaviour, IDamageReceiver
{
    public float height;
    public float Health { get => height; }

    public void ReceiveDamage (float damage, IDamageGiver damageGiver)
    {
        height -= damage;
    }

    public abstract void Die (IDamageGiver damageGiver);
}