using System;
using UnityEngine;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour
{
    public Text interactionUIText;
    public Text triggerUIText;

    internal void ShowInteractionUI (string triggerText)
    {
        if (interactionUIText == null)
        {
            triggerText = "";
        }

        interactionUIText.text = triggerText;
    }

    internal void ShowTriggerUI(string triggerText)
    {
        if (interactionUIText == null)
        {
            triggerText = "";
        }

        triggerUIText.text = triggerText;   
    }
}
