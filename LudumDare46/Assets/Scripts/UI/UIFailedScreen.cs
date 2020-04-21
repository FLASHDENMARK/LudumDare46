using System;
using UnityEngine;
using UnityEngine.UI;

public class UIFailedScreen : MonoBehaviour
{
    public GameObject failedScreen;
    public GameObject successScreen;
    public Text failedText;
    public Text successText;

    public void DisplayFail(string text)
    {
        failedScreen.SetActive(true);
        failedText.text = text;
    }

    internal void DisplaySuccess(string v, bool enable)
    {
        //successScreen.SetActive(enable);
        //successText.text = v;
    }
}
