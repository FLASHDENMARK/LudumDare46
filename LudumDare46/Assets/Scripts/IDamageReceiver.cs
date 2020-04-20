public interface IDamageReceiver
{
    float Health { get; }
    bool IsDead { get; }

    void ReceiveDamage(float damage, IDamageGiver damageGiver);

    void Die(IDamageGiver damageGiver);
    void Alert(ControllerBase damageGiver);
    void SomeoneDied(ControllerBase damageGiver);
}