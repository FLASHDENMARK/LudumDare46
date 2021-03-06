﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HotspotManager : MonoBehaviour
{
    public List<Hotspot> Hotspots;

    // Start is called before the first frame update
    void Awake()
    {
        Hotspots = new List<GameObject>(GameObject.FindGameObjectsWithTag("Hotspot")).Select(hotspot => hotspot.GetComponent<Hotspot>()).ToList();
    }



    public bool RequestHotspot(out Vector3 position, out Hotspot _hotspot)
    {
        _hotspot = null;
        position = Vector3.zero;

        foreach (Hotspot hotspot in Hotspots.Where(hotspot => hotspot.IsAllowed).OrderBy(_ => UnityEngine.Random.Range(0, 100)))
        {
            if(hotspot.TakePosition(out position))
            {
                _hotspot = hotspot;
                return true;
            }
        }

        return false;
    }
}
