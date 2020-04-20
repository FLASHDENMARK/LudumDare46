using Assets.Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NavigationObjective : ObjectiveBase
{
    public List<Navpoint> waypoints = new List<Navpoint>();
    private Navpoint _currentWaypoint;
    public bool startAtClosestWaypoint = false;
    public float nextWaypointDistance = 2.0F;
    public bool loopUntilCompletionTime = false; 

    public override void Begin (Action<ObjectiveOutcome> endCallback)
    {
        // Do not start from the beginning of the waypoints but the one closest to the AI
        if (startAtClosestWaypoint)
        {
            List<Navpoint> orderedWaypoints = waypoints.OrderBy(d => Vector3.Distance(_controller.transform.position, d.transform.position)).ToList();
            _currentWaypoint = orderedWaypoints.FirstOrDefault();
        }
        else
        {
            _currentWaypoint = waypoints[0];
        }

        base.SetNavMeshDestination(_currentWaypoint.transform);

        base.Begin(endCallback);
    }

    protected override void UpdateObjective ()
    {
        if (Vector3.Distance(_currentWaypoint.transform.position, base._controller.transform.position) <= nextWaypointDistance)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        int index = waypoints.IndexOf(_currentWaypoint);

        if (waypoints.Count > index + 1)
        {
            _currentWaypoint = waypoints[index + 1];
            base.SetNavMeshDestination(_currentWaypoint.transform);
        }
        else
        {
            if (loopUntilCompletionTime)
            {
                _currentWaypoint = waypoints[0];
                base.SetNavMeshDestination(_currentWaypoint.transform);
            }
            else
            {
                base.End();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Navpoint previous = null;

        foreach (Navpoint waypoint in waypoints)
        {
            Gizmos.color = Color.white;

            if (previous != null)
            {
                Gizmos.DrawLine(waypoint.transform.position, previous.transform.position);
            }

            Gizmos.color = _color;
            Gizmos.DrawCube(waypoint.transform.position, Vector3.one);

            previous = waypoint;
        }

    }

    public override Vector3 GetBeginningVector()
    {
        return waypoints.Any() ? waypoints.First().transform.position : Vector3.zero;
    }

    public override Vector3 GetEndingVector()
    {
        return waypoints.Any() ? waypoints.Last().transform.position : Vector3.zero;
    }
}
