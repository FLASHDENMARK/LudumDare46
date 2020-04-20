using UnityEngine;

public class WeaponPickupTrigger : TriggerBase
{
    public MeshRenderer rendereToDisable;
    public override void On()
    {
        Destroy(this.gameObject);

        if (base.toggleSound != null)
        {
            AudioSource.PlayClipAtPoint(base.toggleSound, transform.position);
        }
        //base.isEnabled = false;
        //rendereToDisable.enabled = false;
    }

    public override void Off()
    {

    }
}
