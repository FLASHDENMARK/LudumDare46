using Assets.Components.Weapons;
using Assets.Scripts.Managers;
using System;
using UnityEngine;

public class AttackTargetObjective : ObjectiveBase
{
    public ControllerBase target;
    public WeaponBase weapon;
    public float killDistance = 4.0f;
    public ObjectiveBase prerequisite; // This objective must be completed before this objective can be made. It this needed?

    public override void Begin(Action<ObjectiveOutcome> endCallback)
    {
        if (weapon != null)
            weapon.gameObject.SetActive(false);

        base.Begin(endCallback);
        
        if (prerequisite != null && !prerequisite.wasSuccesful)
        {
            base.End(false);
        }
    }

    protected override void UpdateObjective()
    {
        /*if (_controller.pickup == null)
        {
            End(false);

            return;
        }*/


        float distance = Vector3.Distance(target.transform.position, _controller.transform.position);

        base.SetNavMeshDestination(target.transform.position);

        if (weapon != null)
        {
                if (distance <= killDistance)
            {

                weapon.gameObject.SetActive(true);
                weapon.Shoot();
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
        else
        {
            if (distance <= killDistance)
            {
                target.Die(_controller);
            }
                
        }


        if (target.IsDead)
        {
            End(true);
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
