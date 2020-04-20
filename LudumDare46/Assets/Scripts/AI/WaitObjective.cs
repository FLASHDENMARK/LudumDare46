using Assets.Scripts.Managers;
using System;
using UnityEngine;

public class WaitObjective : ObjectiveBase
{
    public float minWait = 3.0F;
    public float maxWait = 5.0F;

    public override void Begin (Action<ObjectiveOutcome> endCallback)
    {
        base.SetNavMeshDestination(transform.position);

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
