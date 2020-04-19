using UnityEngine;

public class AttackTargetObjective : ObjectiveBase
{
    public ControllerBase target;
    public float killDistance = 2.0f;
    public ObjectiveBase prerequisite; // This objective must be completed before this objective can be made. It this needed?

    protected override void UpdateObjective()
    {
        if (_controller.pickup == null)
        {
            End();

            return;
        }


        float distance = Vector3.Distance(target.transform.position, _controller.transform.position);

        base.SetNavMeshDestination(target.transform.position);
        
        if (distance <= killDistance)
        {
            target.Die(_controller);

            End();
        }
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
