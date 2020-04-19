using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Just changes the texture of screens
/// </summary>
public class SecurityConsoleTrigger : TriggerBase
{
    public List<SecurityCamera> cameras = new List<SecurityCamera>();
    public List<GameObject> screens = new List<GameObject>();
    public List<Material> noiseGrain = new List<Material>();

    public float noiseGrainChangeRate = 0.05F;
    private float _noiseGrainChangeRate = 0.05F;
    private int _noiseGrainIndex = 0;

    /*private void Awake()
    {
        _noiseGrainChangeRate = noiseGrainChangeRate;
    }*/
    
    private void Update()
    {
        _noiseGrainChangeRate -= Time.deltaTime;

        if (!on)
        {
            if (_noiseGrainChangeRate <= 0)
            {
                _noiseGrainIndex += 1;
                _noiseGrainIndex %= noiseGrain.Count;

                foreach (GameObject screen in screens)
                {
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
