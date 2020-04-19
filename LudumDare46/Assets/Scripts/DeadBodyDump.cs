using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBodyDump : MonoBehaviour
{
    public AudioClip AudioClip;

    private void OnTriggerEnter (Collider other)
    {
        if (other.name == "corpse")
        {
            
            Destroy(other.gameObject);
        }
    }
}
