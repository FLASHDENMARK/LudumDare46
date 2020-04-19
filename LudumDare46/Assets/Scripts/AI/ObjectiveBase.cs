using Assets.Scripts.Managers;
using System;
using UnityEngine;

public abstract class ObjectiveBase : MonoBehaviour
{
    public bool inProgress;
    public bool isComplete;
    public float completionTime = 20.0F;
    private float _completionTime;
    protected Color _color;
    Action<ObjectiveOutcome> _endCallback;
    protected AIController _controller;

    public abstract Vector3 GetBeginningVector();
    public abstract Vector3 GetEndingVector();
    protected virtual void UpdateObjective() { }


    internal void Initialize (AIController controller, Color color)
    {
        _controller = controller;
        _color = color;
    }

    private void Awake ()
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
        _controller.SetNavMeshDestination(position);
    }

    internal void SetNavMeshDestination(Transform transform)
    {
        SetNavMeshDestination(transform.position);
    }


    public virtual void Begin (Action<ObjectiveOutcome> endCallback)
    {
        if (!inProgress)
        {
            this.completionTime = _completionTime;
            this.enabled = true;
            this._endCallback = endCallback;

            inProgress = true;
            isComplete = false;
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
