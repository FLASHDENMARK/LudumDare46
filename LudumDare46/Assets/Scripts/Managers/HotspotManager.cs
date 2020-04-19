using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotspotManager : MonoBehaviour
{
    public List<GameObject> Hotspots;

    // Start is called before the first frame update
    void Start()
    {
        Hotspots = new List<GameObject>(GameObject.FindGameObjectsWithTag("Hotspot"));
    }
}
