using UnityEngine;

[System.Serializable]
public class SecurityCamera : MonoBehaviour
{
    public RenderTexture renderTexture;
    public Material renderMaterial;

    public void SetCameraState(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }
}
