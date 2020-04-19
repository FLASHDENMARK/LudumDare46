using UnityEngine;

public class PickupObjective : ObjectiveBase
{
    public Pickupable pickup;
    public float pickupDistance = 4.0F;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = _color;
        Gizmos.DrawSphere(pickup.transform.position, pickupDistance);
    }

    public override Vector3 GetBeginningVector()
    {
        return pickup.transform.position;
    }

    public override Vector3 GetEndingVector()
    {
        return pickup.transform.position;
    }
}
