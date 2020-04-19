using System;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private static UISubtitles _subtitles;
    private static UIControls _controls;

    private void Awake()
    {
        _subtitles = GetComponent<UISubtitles>();
        _controls = GetComponent<UIControls>();
    }

    public static void DisplaySubtitles (string speaker, string words, float duration = 10.0F)
    {
        _subtitles.DisplaySubtitles(speaker, words, duration);
    }

    internal static void DisplayControls (bool doDisplay)
    {
        _controls.DisplayControls(doDisplay);
    }
}
