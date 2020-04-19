using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    bool _triggerUI = false;
    public InteractionBase interactable;

    private void OnTriggerEnter (Collider other)
    {
        _triggerUI = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _triggerUI = false;
    }

    private void OnGUI()
    {
        // This is dumb but it work. And if it works...
        if (_triggerUI)
        {
            string onOff = interactable.on ? "off" : "on";
            string text = $"Press E to switch {interactable.gameObject.name} " + onOff;

            GUI.Label(new Rect(30, 30, 400, 30), text);
        }
    }

    void Update ()
    {
        // This is dumb but it work. And if it works...
        if (_triggerUI && Input.GetKeyDown(KeyCode.E))
        {
            interactable.ToggleUse();
        }
    }
}
