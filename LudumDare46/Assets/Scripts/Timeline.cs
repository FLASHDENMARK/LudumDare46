using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Timeline : MonoBehaviour
{
    [SerializeField]
    public List<Deviation> deviations = new List<Deviation>();
    AIController civilian;


    // Start is called before the first frame update
    void Start()
    {
        civilian = GetComponent<AIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deviations.Any())
        {
            Deviation detour = deviations.Find(deviation => Time.time >= deviation.Starttime);
            if(detour != null)
            {
                civilian.IsOverridden = true;
                StartCoroutine(RunDeviation(detour));
            }
        }
    }


    IEnumerator RunDeviation(Deviation detour)
    {
        while (detour.Waypoints.Count > 0)
        {
            float curTime = (Time.time - detour.Starttime);
            Waypoint waypoint = detour.Waypoints.Find(wp => curTime >= wp.ExecutionTime);
            if (waypoint != null)
            {
                civilian.SetNavMeshDestination(waypoint.Position);
                detour.Waypoints.Remove(waypoint);
            }
            yield return new WaitForSeconds(0.05f);
        }
        deviations.Remove(detour);
        civilian.IsOverridden = false;
    }

    private void OnDrawGizmosSelected()
    {
        foreach(Deviation deviation in deviations)
        {
            Gizmos.color = deviation.color;
            for (int i = 0; i < deviation.Waypoints.Count; i++)
            {
                Gizmos.DrawWireSphere(deviation.Waypoints[i].Position, 0.5f);

                if (i < deviation.Waypoints.Count - 1)
                {
                    Gizmos.DrawLine(deviation.Waypoints[i].Position, deviation.Waypoints[i + 1].Position);
                }
            }
        }

        Gizmos.color = Color.white;

    }

}

[Serializable]
public class Deviation
{
    [SerializeField]
    public List<Waypoint> Waypoints;
    public float Starttime;
    public Color color;
}

[Serializable]
public class Waypoint
{
    public Vector3 Position = new Vector3(1f, 0f, 2f);
    public float ExecutionTime;
}
