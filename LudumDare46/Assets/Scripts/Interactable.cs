using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Interactable : MonoBehaviour
{
    public bool CanBePickedUp = false;
    public Rigidbody RB;

    private void Awake()
    {

        if(CanBePickedUp && GetComponent<Rigidbody>() == null)
        {
            RB = gameObject.AddComponent<Rigidbody>();
        } else {
            RB = GetComponent<Rigidbody>();
        }

        RB.interpolation = RigidbodyInterpolation.Interpolate;
    }
}
