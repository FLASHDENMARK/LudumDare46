using Assets.Scripts.Managers;
using System;
using UnityEngine;

public class TriggerObjective : ObjectiveBase
{
    public TriggerBase trigger;
    public float triggerRange = 3.0F;
    private bool _hasTriggeredOnce = false;

    public override bool Begin(Action<ObjectiveOutcome> endCallback)
    {
        base.SetNavMeshDestination(trigger.transform);

        return base.Begin(endCallback);
    }

    public float distanceTest;

    protected override void UpdateObjective()
    {
        distanceTest = Vector3.Distance(_controller.transform.position, trigger.transform.position);

        if (distanceTest <= triggerRange)
        {
            _controller.OnPlayerInsideInteractionTrigger(trigger);

            if (_controller.trigger != null && !_hasTriggeredOnce)
            {
                _hasTriggeredOnce = true;
                trigger.ToggleUse();

                End();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.6F);

        Vector3 center = trigger.transform.position;
        center.y += 0.25f;
        Gizmos.DrawSphere(center, triggerRange);
    }

    public override void Reset()
    {
        base.Reset();

        _hasTriggeredOnce = false;
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
