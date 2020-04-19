using Assets.Scripts.Managers;
using System;
using UnityEngine;

public enum StateEquilibrium { On, Off, Both }

public class TriggerObjective : ObjectiveBase
{
    public StateEquilibrium stateEquilibrium = StateEquilibrium.Both;
    public TriggerBase trigger;
    public float triggerRange = 3.0F;

    public override void Begin(Action<ObjectiveOutcome> endCallback)
    {
        base.SetNavMeshDestination(trigger.transform);

        base.Begin(endCallback);
    }

    protected override void UpdateObjective()
    {
        float distance = Vector3.Distance(_controller.transform.position, trigger.transform.position);

        if (distance <= triggerRange)
        {
            _controller.OnPlayerInsideInteractionTrigger(trigger);

            ToggleTrigger();
        }
        else
        {
            _controller.OnPlayerOutsideInteractionTrigger();
        }
    }

    private void ToggleTrigger ()
    {
        switch (stateEquilibrium)
        {
            case StateEquilibrium.On:

                if (!trigger.on)
                {
                    trigger.ToggleUse();
                }

                break;
            case StateEquilibrium.Off:

                if (trigger.on)
                {
                    trigger.ToggleUse();
                }

                break;
            case StateEquilibrium.Both:

                trigger.ToggleUse();

                break;
        }

        trigger.ToggleUse();

        End();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = _color;

        Gizmos.DrawSphere(transform.position, triggerRange);
    }

    public override void Reset()
    {
        base.Reset();
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
