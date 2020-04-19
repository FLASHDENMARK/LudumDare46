﻿using Assets.Scripts.Managers;
using System;
using UnityEngine;

public abstract class ObjectiveBase : MonoBehaviour
{
    public string description;
    public bool inProgress;
    public bool isComplete;
    public float completionTime;
    private float _completionTime;
    Action<ObjectiveOutcome> _endCallback;
    protected CivilianController _controller;

    public abstract Vector3 GetBeginningVector();
    public abstract Vector3 GetEndingVector();

    protected virtual void UpdateObjective() { }


    internal void Initialize(CivilianController controller)
    {
        _controller = controller;
    }

    private void Awake()
    {
        _completionTime = completionTime;
    }

    void Update ()
    {
        if (_controller.IsDead)
        {
            End();
            return;
        }

        if (inProgress)
        {
            UpdateObjective();
        }
    }

    protected void SetNavMeshDestination (Vector3 position)
    {
        _controller.IsOverridden = true;
        _controller.NavMeshAgent.SetDestination(position);
    }

    internal void SetNavMeshDestination(Transform transform)
    {
        SetNavMeshDestination(transform.position);
    }


    public virtual bool Begin (Action<ObjectiveOutcome> endCallback)
    {
        if (!inProgress)
        {
            this.enabled = true;
            this._endCallback = endCallback;

            inProgress = true;
            isComplete = false;

            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void End (ObjectiveOutcome outcome = null)
    {
        if (inProgress)
        {
            this.enabled = false;
            inProgress = false;
            isComplete = true;

            if (outcome == null)
            {
                _endCallback(new ObjectiveOutcome() { wasSuccessful = true });
            }
            else
            {
                _endCallback(outcome);
            }
        }
    }

    public virtual void Reset()
    {
        completionTime = _completionTime;

        inProgress = false;
        isComplete = false;
    }
}