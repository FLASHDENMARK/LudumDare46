using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LookAtTargetObjective : ObjectiveBase
{
    private bool _active;
    public Material SniperMat;
    public LineRenderer lineRenderer;
    public override Vector3 GetBeginningVector()
    {
        _active = true;
        return Vector3.zero;
    }

    public override Vector3 GetEndingVector()
    {
        _active = false;
        return Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.material = SniperMat;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, _controller.transform.position);
    }
}
