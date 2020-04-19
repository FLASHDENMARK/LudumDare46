using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityConsoleInteraction : InteractionBase
{
    public List<SecurityCamera> cameras = new List<SecurityCamera>();
    public List<GameObject> screens = new List<GameObject>();
    public List<Material> noiseGrain = new List<Material>();

    private void Awake()
    {
        Off();
    }

    public override void ToggleUse()
    {
        on = !on;

        if (on)
        {
            On();
        }
        else
        {
            Off();
        }
    }

    private void Update()
    {
        if (!on)
        {
            foreach (GameObject screen in screens)
            {
                int index = Random.Range(0, noiseGrain.Count);
                screen.GetComponent<MeshRenderer>().material = noiseGrain[index];
            }
        }
    }

    public override void Off()
    {
        // Disable the cameras for better performance
        cameras.ForEach(c => c.SetCameraState(false));
    }

    public override void On ()
    {
        for (int i = 0; i < screens.Count; i++)
        {
            cameras[i].SetCameraState(true);
            screens[i].GetComponent<MeshRenderer>().material = cameras[i].renderMaterial;
        }
    }
}