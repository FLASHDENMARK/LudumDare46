using Assets.Scripts.Managers;
using System;
using UnityEngine;

public class TriggerObjective : ObjectiveBase
{
    public TriggerBase trigger;
    public float triggerRange = 3.0F;
    private bool _hasTriggeredOnce = false;

    public override void Begin(Action<ObjectiveOutcome> endCallback)
    {
        base.SetNavMeshDestination(trigger.transform);

        base.Begin(endCallback);
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
        Gizmos.color = _color;

        Gizmos.DrawSphere(transform.position, triggerRange);
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
