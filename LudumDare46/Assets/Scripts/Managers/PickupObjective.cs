﻿using UnityEngine;

public class PickupObjective : ObjectiveBase
{
    public Pickupable pickup;
    public float pickupDistance = 4.0F;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.6F);

        Vector3 center = pickup.transform.position;
        center.y += 0.25f;
        Gizmos.DrawSphere(center, pickupDistance);
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