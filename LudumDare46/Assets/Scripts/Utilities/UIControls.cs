using UnityEngine;

public class UIControls : MonoBehaviour
{
    public GameObject controls;

    public void DisplayControls(bool doDisplay)
    {
        controls.SetActive(doDisplay);
    }
}