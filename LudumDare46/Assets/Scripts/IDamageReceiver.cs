public interface IDamageReceiver
{
    float Health { get; set; }

    void ReceiveDamage(float damage, IDamageGiver projectileBase);
}