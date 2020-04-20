using System;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private static UISubtitles _subtitles;
    private static UIControls _controls;
    private static UIFailedScreen _failedScreen;

    private void Awake()
    {
        _subtitles = GetComponent<UISubtitles>();
        _controls = GetComponent<UIControls>();
        _failedScreen = GetComponent<UIFailedScreen>();
    }

    public static void DisplaySubtitles (string speaker, string words, float duration = 10.0F)
    {
        _subtitles.DisplaySubtitles(speaker, words, duration);
    }

    internal static void DisplayControls (bool doDisplay)
    {
        _controls.DisplayControls(doDisplay);
    }

    internal static void DisplayFailedScreen (string text)
    {
        _failedScreen.DisplayFail(text);
    }
}
