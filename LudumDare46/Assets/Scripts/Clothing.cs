using UnityEngine;

[System.Serializable]
public class Clothing : MonoBehaviour
{
    public void Initialize (Material material)
    {
        SetPiecesState(false);

        int getRandomPiece = Random.Range(0, transform.childCount);

        Transform piece = transform.GetChild(getRandomPiece);

        MeshRenderer renderer = piece.GetComponent<MeshRenderer>();

        renderer.material = material;

        piece.gameObject.SetActive(true);
    }

    void SetPiecesState (bool isActive)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isActive);
        }
    }
}
