using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;
using static Assets.Scripts.Managers.GameplayManager;

/// <summary>
/// This has to be on a object that does not become inactive or else the watch blinking wont work
/// </summary>
public class Watch : MonoBehaviour
{
    public TextMesh textMesh;
    private static int _hours;
    private static int _minutes;

    private float _timer = 1.0F;
    private float _blinkTimer = 0.0F;

    public static void UpdateWatchTime(int hours, int minutes)
    {
        _hours = hours;
        _minutes = minutes;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (textMesh == null)
        {
            throw new System.Exception("TextMesh cannot be null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer <= 0)
        {
            _blinkTimer = 0.5F;
            _timer = 1;
        }

        _hours = GameplayManager.GameTime.hour;
        _minutes = GameplayManager.GameTime.minute;

        Blink();

        _timer -= Time.deltaTime;
    }

    private void Blink()
    {
        _blinkTimer -= Time.deltaTime;

        string h = IngameTime.Convert(_hours);
        string m = IngameTime.Convert(_minutes);

        if (_blinkTimer >= 0)
        {
            textMesh.text = h + ":" + m;
        }
        else
        {
            textMesh.text = h + " " + m;
        }
    }
}
