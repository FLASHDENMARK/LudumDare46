using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using System;
using UnityEngine;

public class PickupObjective : ObjectiveBase
{
    public Pickupable pickup;
    public float pickupDistance = 4.0F;
    public AudioClip[] onPickup;
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

        if(_controller.GetType() == typeof(AIController))
        {
            if (Vector3.Distance(_controller.transform.position, pickup.transform.position) < 5)
            {
                base.SetNavMeshDestination(pickup.transform);
            }
        } else
        {
            base.SetNavMeshDestination(pickup.transform);
        }




        base.Begin(endCallback);
    }

    protected override void UpdateObjective()
    {
        if (pickup == null)
        {
            base.SetNavMeshDestination(transform.position);

            End();

            return;
        }

        if (Vector3.Distance(pickup.transform.position, _controller.transform.position) <= pickupDistance)
        {
            _controller.Pickup(pickup);

            Utility.PlayAudio(onPickup, _controller.gameObject);

            Destroy(pickup.gameObject);
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
