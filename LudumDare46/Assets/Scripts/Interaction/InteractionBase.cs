using UnityEngine;

public abstract class InteractionBase : MonoBehaviour
{
    public bool on = false;
    public abstract void ToggleUse();

    public abstract void Off();
    public abstract void On();
}
