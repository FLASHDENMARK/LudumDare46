using UnityEngine;
using UnityEngine.UI;

public class UIFailedScreen : MonoBehaviour
{
    public GameObject failedScreen;
    public Text failedText;

    public void DisplayFail(string text)
    {
        failedScreen.SetActive(true);
        failedText.text = text;
    } 
}
