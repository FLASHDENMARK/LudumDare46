public class WeaponPickupTrigger : TriggerBase
{
    public override void On()
    {
        Destroy(this.gameObject);
        
    }

    public override void Off()
    {

    }
}
