using Assets.Scripts.Managers;
using System;
using UnityEngine;

public abstract class ObjectiveBase : MonoBehaviour
{
    public int order = 0;
    public string description;
    public bool inProgress;
    public bool isComplete;
    public float completionTime;
    private float _completionTime;
    // TODO Maek this come from the timeline
    public CivilianController controller;

    public abstract Vector3 GetBeginningVector();
    public abstract Vector3 GetEndingVector();
    public abstract void CheckEndCondition();
    Action<ObjectiveOutcome> endCallback;

    private void Awake()
    {
        _completionTime = completionTime;
    }

    public virtual bool Begin (Action<ObjectiveOutcome> endCallback)
    {
        if (!inProgress)
        {
            this.endCallback = endCallback;

            inProgress = true;
            isComplete = false;

            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void End ()
    {
        if (inProgress)
        {
            inProgress = false;
            isComplete = true;

            endCallback(new ObjectiveOutcome() { wasSuccessful = true }) ;
        }
    }

    public void Reset()
    {
        completionTime = _completionTime;

        inProgress = false;
        isComplete = false;
    }
}
