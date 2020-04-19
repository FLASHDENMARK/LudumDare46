using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityConsoleInteraction : InteractionBase
{
    public List<SecurityCamera> cameras = new List<SecurityCamera>();
    public List<GameObject> screens = new List<GameObject>();
    public List<Material> noiseGrain = new List<Material>();

    public float noiseGrainChangeRate = 0.05F;
    private float _noiseGrainChangeRate = 0.05F;
    private int _noiseGrainIndex = 0;

    private void Awake()
    {
        Off();

        _noiseGrainChangeRate = noiseGrainChangeRate;
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
        _noiseGrainChangeRate -= Time.deltaTime;

        if (!on)
        {
            if (_noiseGrainChangeRate <= 0)
            {
                _noiseGrainIndex += 1; // Random.Range(0, noiseGrain.Count);
                _noiseGrainIndex %= noiseGrain.Count;
                foreach (GameObject screen in screens)
                {
                    Debug.Log("Index " + _noiseGrainIndex);
                    _noiseGrainChangeRate = noiseGrainChangeRate;
                    
                    screen.GetComponent<MeshRenderer>().material = noiseGrain[_noiseGrainIndex];
                }
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
