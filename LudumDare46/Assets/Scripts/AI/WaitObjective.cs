using Assets.Scripts.Managers;
using System;
using UnityEngine;

public class WaitObjective : ObjectiveBase
{
    public Transform WaitPosition;

    public override void Begin (Action<ObjectiveOutcome> endCallback)
    {
        if(WaitPosition == null)
        {
            base.SetNavMeshDestination(transform.position);
        } else
        {
            base.SetNavMeshDestination(WaitPosition.position);
        }

        base.Begin(endCallback);
    }

    protected override void UpdateObjective ()
    {

    }

    public override Vector3 GetBeginningVector()
    {
        return transform.position;
    }

    public override Vector3 GetEndingVector()
    {
        return transform.position;
    }
}
