using UnityEngine;

public class Navpoint : MonoBehaviour
{
    private void OnDrawGizmos ()
    {
        Gizmos.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.30F);

        Vector3 center = transform.position;
        center.y += 0.25f;
        Gizmos.DrawCube(center, Vector3.one * 1f);
    }
}
