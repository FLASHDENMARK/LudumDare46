using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotspotManager : MonoBehaviour
{
    public List<GameObject> hotspots;

    // Start is called before the first frame update
    void Start()
    {
        hotspots = new List<GameObject>(GameObject.FindGameObjectsWithTag("Hotspot"));
    }
}
