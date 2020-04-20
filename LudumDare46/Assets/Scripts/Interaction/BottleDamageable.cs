using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleDamageable : MonoBehaviour, IDamageReceiver
{

    public float health;
    public float Health { get => health;}

    public bool IsDead => throw new System.Exception();

    public void Alert(ControllerBase damageGiver)
    {
        
    }

    public void Die(IDamageGiver damageGiver)
    {
        throw new System.NotImplementedException();
    }

    public void ReceiveDamage(float damage, IDamageGiver damageGiver)
    {
        Destroy(this.gameObject);
    }

    public void SomeoneDied(ControllerBase damageGiver)
    {
    }
}
