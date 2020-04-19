using Assets.Scripts.Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectiveTimeline : MonoBehaviour
{
    // The AI in charge of this timeline

    public CivilianController controller;
    public List<ObjectiveBase> objectives;
    private ObjectiveBase _currentObjective;

    public bool loopTimeline;

    private void Awake()
    {
        objectives.ForEach(o => o.Initialize(controller));

        if (_currentObjective == null)
        {
            _currentObjective = objectives.First();
            _currentObjective.Begin(OnObjectiveCompleted);
        }
    }

    private void OnObjectiveCompleted (ObjectiveOutcome outcome)
    {
        if (outcome.controllerDied)
        {
            _currentObjective = null;
        }

        int current = objectives.IndexOf(_currentObjective);

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
