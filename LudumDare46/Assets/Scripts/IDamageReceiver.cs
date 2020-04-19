public interface IDamageReceiver
{
    float Health { get; }
    bool IsDead { get; }

    void ReceiveDamage(float damage, IDamageGiver damageGiver);

    void Die(IDamageGiver damageGiver);
}