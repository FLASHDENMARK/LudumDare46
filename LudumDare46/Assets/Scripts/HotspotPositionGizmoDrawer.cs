using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotspotPositionGizmoDrawer : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Vector3 center = this.transform.position;
        center.y += 0.25f;
        Gizmos.DrawCube(center, Vector3.one * 0.5f);
    }
}
