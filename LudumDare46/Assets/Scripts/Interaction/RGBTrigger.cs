using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RGB
{
    public Light light;
    public int currentLightIndex;
    public Color initialColor;

    public void SetColor (Color color)
    {
        light.color = color;
    }

    public void SetInitialColor ()
    {
        initialColor = light.color;
    }
}

public class RGBTrigger : TriggerBase
{
    public List<RGB> lights = new List<RGB>();
    public float intervalTime = 0.33F;
    private float _intervalTime;
    public List<Color> sequence = new List<Color>();

    public Transform lever;
    public float leverRotateSpeed;
    public Transform onRotation;
    public Transform offRotation;

    private Quaternion _currentRotation;

    protected override void Awake()
    {
        base.Awake();

        
    }

    private void Start()
    {
        lights.ForEach(l => l.SetInitialColor());
    }

    public override void On ()
    {
        _currentRotation = onRotation.rotation;
        _intervalTime = intervalTime;
    }

    public override void Off()
    {
        _currentRotation = offRotation.rotation;

        // Set the colors back to the original colors
        lights.ForEach(l => l.SetColor(l.initialColor));
    }

    private void Update ()
    {
        if (on)
        {
            _intervalTime -= Time.deltaTime;

            if (_intervalTime <= 0)
            {
                ChangeColor();
                _intervalTime = intervalTime;
            }
        }

        lever.rotation = Quaternion.Lerp(lever.rotation, _currentRotation, leverRotateSpeed * Time.deltaTime);
    }

    private void ChangeColor ()
    {
        foreach (RGB rgb in lights)
        {
            rgb.currentLightIndex++;

            rgb.currentLightIndex %= sequence.Count;

            rgb.SetColor(sequence[rgb.currentLightIndex]);
        }
    }
}
