using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjective : ObjectiveBase
{
    public TriggerBase trigger;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.6F);

        Vector3 center = trigger.transform.position;
        center.y += 0.25f;
        Gizmos.DrawSphere(center, 4);
    }
    public override void CheckEndCondition ()
    {
        // Check if within
        throw new System.NotImplementedException();
    }

    public override Vector3 GetBeginningVector()
    {
        return trigger.transform.position;
    }

    public override Vector3 GetEndingVector()
    {
        return trigger.transform.position;
    }
}
