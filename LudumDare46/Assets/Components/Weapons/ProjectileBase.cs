using UnityEngine;

public class ProjectileBase : MonoBehaviour, IDamageGiver
{
    public float damage = 100;
    public float timer = 20;

    public Transform Transform { get; private set; }

    public void Initialize (/*ISpawnParameters spawnParameters*/)
    {
        /*ProjectileSpawnParameters parameters = (ProjectileSpawnParameters)spawnParameters;

        this.Transform = parameters.Shooter;*/
    }

    private void Update ()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            OnCollisionEnter(null);
        }
    }

    protected virtual void OnCollisionEnter (Collision collision)
    {
        if (collision != null && collision.collider != null)
        {
            IDamageReceiver damageable = collision.collider.GetComponent<IDamageReceiver>();

            if (damageable != null)
            {
                ApplyDamage(damageable, damage);
            }
        }

        Destroy(this.gameObject);
    }

    public void ApplyDamage(IDamageReceiver damageable, float damage)
    {
        damageable.ReceiveDamage(damage, this);
    }

    public void ReceiveDamage(float damage, IDamageGiver attacker)
    {
        
    }

    public void Die()
    {
        
    }
}
