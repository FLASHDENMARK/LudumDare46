using System;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private static UISubtitles _subtitles;
    private static UIControls _controls;
    private static UIFailedScreen _failedScreen;
    public static UIInteraction _interactionUI;

    private void Awake()
    {
        _subtitles = GetComponent<UISubtitles>();
        _controls = GetComponent<UIControls>();
        _failedScreen = GetComponent<UIFailedScreen>();
        _interactionUI = GetComponent<UIInteraction>();
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

    internal static void ShowInteractionUI(string triggerText)
    {
        _interactionUI.ShowInteractionUI(triggerText);
    }

    internal static void ShowTriggerUI(string triggerText)
    {
        _interactionUI.ShowTriggerUI(triggerText);
    }

    static float successScreenTime = 8.0F;

    private void Update()
    {
        successScreenTime -= Time.deltaTime;

        if (successScreenTime <= 0)
        {
            _failedScreen.DisplaySuccess("", false);
        }
    }

    public static void DisplaySuccessScreen(string v)
    {
        successScreenTime = 10.0F;

        _failedScreen.DisplaySuccess(v, true);
    }
}
