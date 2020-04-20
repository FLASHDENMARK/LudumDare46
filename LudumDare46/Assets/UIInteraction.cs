using System;
using UnityEngine;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour
{
    public GameObject interacationUIBackground;
    public Text interactionUIText;
    public GameObject triggerUIBackground;
    public Text triggerUIText;

    internal void ShowInteractionUI (string triggerText)
    {
        interacationUIBackground.SetActive(triggerText == "" ? false : true);

        if (interactionUIText == null)
        {
            triggerText = "";
        }

        interactionUIText.text = triggerText;
    }

    internal void ShowTriggerUI(string triggerText)
    {
        triggerUIBackground.SetActive(triggerText == "" ? false : true);
        
        if (interactionUIText == null)
        {
            triggerText = "";
        }

        triggerUIText.text = triggerText;   
    }
}
