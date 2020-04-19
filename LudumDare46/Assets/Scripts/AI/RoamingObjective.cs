using Assets.Scripts.Managers;
using System;
using UnityEngine;

public class RoamingObjective : ObjectiveBase
{
    public override void Begin (Action<ObjectiveOutcome> endCallback)
    {
        _controller.IsOverridden = false;

        base.Begin(endCallback);
    }

    protected override void UpdateObjective ()
    {

    }

    private void OnDrawGizmosSelected ()
    {

    }

    public override Vector3 GetBeginningVector()
    {
        // TODO REturning zero here does not make that much sense
        return Vector3.zero;
    }

    public override Vector3 GetEndingVector()
    {
        // TODO REturning zero here does not make that much sense
        return Vector3.zero;
    }
}
