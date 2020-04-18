using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
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


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        CheckPickup();



    }


    private void FixedUpdate()
    {
        if (PickedUpInteractable != null)
        {
            Hold();
            Rotate();
        }
    }


    private void CheckPickup()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray aimRay = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.SphereCast(aimRay, spherecastRadius, out hit, SpherecastDistance, InteractableMask))
            {
                PickUp(hit.transform.GetComponent<Interactable>());
            }

            Vector2 MouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * 100f * Time.deltaTime;
            DesiredRotation.x += MouseInput.y;
            DesiredRotation.y += MouseInput.x;
        }
    }

    private void Rotate()
    {
        if (Input.GetMouseButton(1))
        {
            PickedUpInteractable.transform.LookAt(transform);
            PickedUpInteractable.transform.Rotate(DesiredRotation);
        }
    }

    private void Hold()
    {
        float freeDist = FreeSpaceDist();
        Vector3 DesiredHoldPos = transform.position + (cam.transform.forward * freeDist);
        if(Vector3.Distance(PickedUpInteractable.transform.position, DesiredHoldPos) > SnappingThreshold)
        {
            PickedUpInteractable.transform.position = DesiredHoldPos;
        }

        if (Vector3.Distance(PickedUpInteractable.transform.position, DesiredHoldPos) > MovementThreshold)
        {
            Vector3 movementVector = DesiredHoldPos - PickedUpInteractable.transform.position;
            PickedUpInteractable.RB.velocity = movementVector * Mathf.Clamp(100 * movementVector.magnitude, 0, SmoothingAmount);
        }
    }

    float FreeSpaceDist()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, cam.transform.forward, out hit, HoldDistance, EnvironmentMask))
        {
            return Vector3.Distance(transform.position, hit.point);
        } else
        {
            return HoldDistance;
        }
    }

    void PickUp(Interactable item)
    {
        DesiredRotation = Vector3.zero;
        PickedUpInteractable = item;
        item.transform.LookAt(transform.position);
    }




}
