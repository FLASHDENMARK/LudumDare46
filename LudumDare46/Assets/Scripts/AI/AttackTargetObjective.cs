using UnityEngine;

public class AttackTargetObjective : ObjectiveBase
{
    public Transform target;
    public ObjectiveBase prerequisite; // This objective must be completed before this objective can be made. It this needed?

    protected override void UpdateObjective()
    {
        base.SetNavMeshDestination(target.position);
    }

    public override Vector3 GetBeginningVector()
    {
        return target.transform.position;
    }

    public override Vector3 GetEndingVector()
    {
        return target.transform.position;
    }
}
