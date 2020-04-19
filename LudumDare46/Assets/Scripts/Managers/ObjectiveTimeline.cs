using Assets.Scripts.Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectiveTimeline : MonoBehaviour
{
    // The AI in charge of this timeline

    public CivilianController controller;
    public List<ObjectiveBase> objectives;
    public ObjectiveBase _currentObjective;

    public float timer = 0.0F;

    public bool loopTimeline;

    private void Awake()
    {
        if (_currentObjective == null)
        {
            _currentObjective = objectives.First();
            _currentObjective.Begin(OnObjectiveCompleted);
        }
    }

    private void OnObjectiveCompleted (ObjectiveOutcome outcome)
    {
        int current = _currentObjective.order;

        if (objectives.Count > current + 1)
        {
            _currentObjective = objectives[current + 1];
            _currentObjective.Begin(OnObjectiveCompleted);
        }
        else
        {
            if (loopTimeline)
            {
                objectives.ForEach(o => o.Reset());
                _currentObjective = objectives.First();

                _currentObjective.Begin(OnObjectiveCompleted);
            }
            else
            {
                HUD.DisplaySubtitles("", "Timeline is done");
            }
        }
    }

    void Update ()
    {
        //timer += Time.deltaTime;
        if (_currentObjective)
        {
            _currentObjective.completionTime -= Time.deltaTime;

            if (_currentObjective.completionTime <= 0)
            {
                _currentObjective.End();
            }
        }
    }

    private void OnDrawGizmosSelected ()
    {
        ObjectiveBase previous = null;

        foreach (ObjectiveBase objective in objectives)
        {
            if (previous != null)
            {
                Gizmos.DrawLine(objective.GetBeginningVector(), previous.GetEndingVector());
            }

            previous = objective;
        }

        if (loopTimeline)
        {
            Gizmos.DrawLine(objectives.First().GetBeginningVector(), previous.GetEndingVector());
        }
    }
}
