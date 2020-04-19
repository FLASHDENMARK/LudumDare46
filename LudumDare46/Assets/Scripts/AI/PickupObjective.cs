using Assets.Scripts.Managers;
using System;
using UnityEngine;

public class PickupObjective : ObjectiveBase
{
    public Pickupable pickup;
    public float pickupDistance = 4.0F;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = _color;
        if (pickup != null)
        {
            Gizmos.DrawSphere(pickup.transform.position, pickupDistance);
        }
    }

    public override void Begin(Action<ObjectiveOutcome> endCallback)
    {
        if (pickup == null)
        {
            End();

            return;
        }

        base.SetNavMeshDestination(pickup.transform);

        base.Begin(endCallback);
    }

    protected override void UpdateObjective()
    {
        if (pickup == null)
        {
            End();

            return;
        }

        if (Vector3.Distance(pickup.transform.position, _controller.transform.position) <= pickupDistance)
        {
            _controller.Pickup(pickup);

            End();
        }
    }

    public override Vector3 GetBeginningVector()
    {
        if (pickup != null)
        {
            return pickup.transform.position;
        }
        else
        {
            // TODO fix
            return Vector3.zero;
        }
    }

    public override Vector3 GetEndingVector()
    {
        if (pickup != null)
        {
            return pickup.transform.position;
        }
        else
        {
            // TODO fix
            return Vector3.zero;
        }
    }
}
