using Assets.Components.Weapons;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using System;
using UnityEngine;

public class AttackTargetObjective : ObjectiveBase
{
    public ControllerBase target;
    public float killDistance = 4.0f;
    public float pullWeaponDistance = 8.0F;
    public float consealWeaponAfterKillWait = 4.0F;
    private float _consealWeaponAfterKillWait = 4.0F;
    public ObjectiveBase prerequisite; // This objective must be completed before this objective can be made. It this needed?

    public float distanceToTarget;
    public float angleToTarget;
    public float attackAngle = 5.0F;

    public override void Begin(Action<ObjectiveOutcome> endCallback)
    {
        base.Begin(endCallback);

        _consealWeaponAfterKillWait = consealWeaponAfterKillWait;

        if (_controller.currentWeapon == null || target.IsDead)
        {
            base.End(false);
        }

        if (pullWeaponDistance < killDistance)
        {
            throw new Exception("Pull weapon distance should noit be less than shoot distance");
        }

        if (prerequisite != null && !prerequisite.wasSuccesful)
        {
            base.End(false);
        }
    }
    
    protected override void UpdateObjective()
    {
        distanceToTarget = Vector3.Distance(target.transform.position, _controller.transform.position);
        angleToTarget = Utility.GetAngle(_controller.transform, target.transform);

        if (!target.IsDead)
        {
            base.SetNavMeshDestination(target.transform.position);

            if (distanceToTarget <= pullWeaponDistance)
            {
                _controller.currentWeapon.gameObject.SetActive(true);
            }
            else
            {
                _controller.currentWeapon.gameObject.SetActive(false);
            }

            if (distanceToTarget <= killDistance && angleToTarget <= attackAngle)
            {
                // dO NOT MOVE ANYMORE
                base.SetNavMeshDestination(transform.position);
                _controller.ShootWeapon();
            }
        }

        if (target.IsDead)
        {
            _consealWeaponAfterKillWait -= Time.deltaTime;

            if (_consealWeaponAfterKillWait <= 0)
            {
                _controller.currentWeapon.gameObject.SetActive(false);
                
                End(true);
            }
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
