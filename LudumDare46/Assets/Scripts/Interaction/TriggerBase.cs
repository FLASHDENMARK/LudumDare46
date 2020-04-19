using UnityEngine;

public abstract class TriggerBase : MonoBehaviour
{
    public bool on = false;

    /// <summary>
    /// Is this trigger enabled
    /// </summary>
    public bool isEnabled = true;

    public string triggerOnText;

    public string triggerOffText;

    public virtual void Off() { }

    public virtual void On () { }

    protected virtual void Awake()
    {
        if (on)
        {
            On();
        }
        else
        {
            Off();
        }
    }

    public virtual void ToggleUse ()
    {
        on = !on;

        if (on)
        {
            On();
        }
        else
        {
            Off();
        }
    }

    private void OnTriggerEnter (Collider collider)
    {
        ControllerBase controller = collider.GetComponent<ControllerBase>();

        if (controller != null)
        {
            controller.OnPlayerInsideInteractionTrigger(this);
        }
    }

    private void OnTriggerExit (Collider collider)
    {
        PlayerController controller = collider.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.OnPlayerOutsideInteractionTrigger();
        }
    }
}
