using Assets.Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NavigationObjective : ObjectiveBase
{
    public List<Navpoint> waypoints = new List<Navpoint>();

    public override bool Begin(Action<ObjectiveOutcome> endCallback)
    {
        var orderedWaypoints = waypoints.OrderBy(d => Vector3.Distance(controller.transform.position, d.transform.position));

        Navpoint closestToCurrentRoam = orderedWaypoints.FirstOrDefault();

        //controller.



        return base.Begin(endCallback);


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

    public override void CheckEndCondition ()
    {
        /*if (controller is within last waypoint)
        {

        }*/
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
