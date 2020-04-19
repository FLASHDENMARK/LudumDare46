using Assets.Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NavigationObjective : ObjectiveBase
{
    public List<Navpoint> waypoints = new List<Navpoint>();
    public Navpoint currentWaypoint;
    public float nextWaypointDistance = 2.0F;

    public override bool Begin(Action<ObjectiveOutcome> endCallback)
    {
        List<Navpoint> orderedWaypoints = waypoints.OrderBy(d => Vector3.Distance(_controller.transform.position, d.transform.position)).ToList();

        currentWaypoint = orderedWaypoints.FirstOrDefault();

        base.SetNavMeshDestination(currentWaypoint.transform.position);

        return base.Begin(endCallback);
    }

    protected override void UpdateObjective ()
    {
        if (Vector3.Distance(currentWaypoint.transform.position, base._controller.transform.position) <= nextWaypointDistance)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        int index = waypoints.IndexOf(currentWaypoint);

        if (waypoints.Count > index + 1)
        {
            currentWaypoint = waypoints[index + 1];
            base.SetNavMeshDestination(currentWaypoint.transform);
        }
        else
        {
            base.End();
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

            Gizmos.color = Color.red;
            Vector3 center = waypoint.transform.position;
            center.y += 0.25f;
            Gizmos.DrawCube(center, Vector3.one * 1f);

            previous = waypoint;
        }

    }

    public override Vector3 GetBeginningVector()
    {
        return waypoints.First().transform.position;
    }

    public override Vector3 GetEndingVector()
    {
        return waypoints.Last().transform.position;
    }
}
