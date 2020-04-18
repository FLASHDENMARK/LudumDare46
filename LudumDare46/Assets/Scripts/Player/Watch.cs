using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        Blink();

        _timer -= Time.deltaTime;
    }

    private void Blink()
    {
        _blinkTimer -= Time.deltaTime;

        string h;
        string m;

        // This is bad. But it works
        if (_hours < 10)
        {
            h = "0" + _hours;
        }
        else if (_hours == 0)
        {
            h = "00";
        }
        else
        {
            h = _hours.ToString();
        }

        if (_minutes < 10)
        {
            m = "0" + _minutes;
        }
        else if (_minutes == 0)
        {
            m = "00";
        }
        else
        {
            m = _minutes.ToString();
        }

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
