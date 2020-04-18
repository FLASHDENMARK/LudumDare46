public interface IDamageReceiver
{
    float Health { get; }

    void ReceiveDamage(float damage, IDamageGiver damageGiver);

    void Die(IDamageGiver damageGiver);
}