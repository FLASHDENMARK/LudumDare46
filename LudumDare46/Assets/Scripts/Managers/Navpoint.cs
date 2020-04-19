using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navpoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 center = transform.position;
        center.y += 0.25f;
        Gizmos.DrawCube(center, Vector3.one * 1f);
    }
}
