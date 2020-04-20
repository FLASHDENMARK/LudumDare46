using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InteractableController : MonoBehaviour
{

    public Camera cam;
    Interactable PickedUpInteractable;

    [Header("Interaction Settings")]
    public float SpherecastDistance; 
    public float spherecastRadius;
    [Header("Lifting Settings")]
    public float MovementThreshold;
    public float SnappingThreshold, SmoothingAmount, HoldDistance;
    public LayerMask EnvironmentMask, InteractableMask;

    private Vector3 DesiredRotation;

    public bool isHoldingSuspiciousItem => PickedUpInteractable == null ? false : PickedUpInteractable.isSuspicious;
    public string suspiciousItemName => PickedUpInteractable == null ? null : PickedUpInteractable.name;

    private AudioSource yeet;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        yeet = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PickedUpInteractable != null)
        {
            RotateObject();
        }
    }


    public bool InteractionPossible()
    {
        Ray aimRay = new Ray(cam.transform.position, cam.transform.forward);// cam.ScreenPointToRay(Input.mousePosition);
        return Physics.SphereCast(aimRay, spherecastRadius, SpherecastDistance, InteractableMask);
    }

    public void LetGo()
    {
        if(PickedUpInteractable != null)
        {
            if(PickedUpInteractable.RB != null)
            {
                PickedUpInteractable.RB.useGravity = true;
                
                if (PickedUpInteractable.RB.velocity.magnitude > 12f) {
                    yeet.Play();
                }
            }
            PickedUpInteractable = null;
        }
    }

    private void FixedUpdate()
    {
        if (PickedUpInteractable != null)
        {
            Hold();
        }
    }


    public void Rotate(Vector2 input)
    {
        DesiredRotation.x += input.y;
        DesiredRotation.y += input.x;
    }

    private void Hold()
    {
        if (PickedUpInteractable.RB == null)
        {
            LetGo();
            return;
        }
            


        float freeDist = FreeSpaceDist();
        Vector3 DesiredHoldPos = cam.transform.position + (cam.transform.forward * freeDist);

        if(Vector3.Distance(PickedUpInteractable.transform.position, DesiredHoldPos) > SnappingThreshold)
        {
            PickedUpInteractable.transform.position = DesiredHoldPos;
        }

        if (Vector3.Distance(PickedUpInteractable.transform.position, DesiredHoldPos) > MovementThreshold)
        {
            Vector3 movementVector = DesiredHoldPos - PickedUpInteractable.transform.position;
            PickedUpInteractable.RB.velocity = movementVector * Mathf.Clamp(100 * movementVector.magnitude, 0, SmoothingAmount);
        } else
        {
            PickedUpInteractable.RB.velocity = Vector3.zero;
        }
    }

    private void RotateObject()
    {
        PickedUpInteractable.transform.LookAt(transform);
        PickedUpInteractable.transform.Rotate(DesiredRotation);
    }

    float FreeSpaceDist()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, HoldDistance, EnvironmentMask))
        {
            return Vector3.Distance(cam.transform.position, hit.point);
        } 
        else
        {
            return HoldDistance;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(cam.transform.position, cam.transform.forward * SpherecastDistance);
        Gizmos.DrawWireSphere(cam.transform.position + cam.transform.forward * SpherecastDistance, spherecastRadius);
    }


    public void PickUp()
    {
        Ray aimRay = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        Interactable item;

        if (Physics.SphereCast(aimRay, spherecastRadius, out hit, SpherecastDistance, InteractableMask))
        {
            item = hit.transform.GetComponent<Interactable>();
            DesiredRotation = Vector3.zero;
            item.transform.parent = null;
            PickedUpInteractable = item;
            PickedUpInteractable.RB.isKinematic = false;
            PickedUpInteractable.RB.useGravity = false;
        }

    }
}
