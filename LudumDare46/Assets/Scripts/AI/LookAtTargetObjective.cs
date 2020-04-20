using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LookAtTargetObjective : ObjectiveBase
{
    private bool _active;
    public Material SniperMat;
    public LineRenderer lineRenderer;
    public ControllerBase Target;
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

    public override void End(bool success = false)
    {
        
        lineRenderer.SetPosition(0, _controller.transform.position);
        lineRenderer.SetPosition(1, _controller.transform.position);

        RaycastHit hit;
        Ray ray = new Ray(_controller.transform.position, Target.transform.position - _controller.transform.position);

        if (Physics.Raycast(ray, out hit))
        {
            ControllerBase CB = hit.transform.GetComponent<ControllerBase>();
            if(CB != null && CB == Target)
            {
                success = true;
                Target.Die(_controller);
            }
        }

        base.End(success);

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

        RaycastHit hit;
        Ray ray = new Ray(_controller.transform.position, Target.transform.position - _controller.transform.position);

        if(Physics.Raycast(ray, out hit))
        {
            lineRenderer.SetPosition(0, _controller.transform.position);
            lineRenderer.SetPosition(1, hit.point);
        } else
        {
            lineRenderer.SetPosition(0, _controller.transform.position);
            lineRenderer.SetPosition(1, Target.transform.position);
        }



    }
}
