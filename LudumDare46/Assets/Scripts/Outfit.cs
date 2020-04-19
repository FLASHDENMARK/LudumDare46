using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An outfit represents a collection of different clothing
/// </summary>
public class Outfit : MonoBehaviour
{
    public List<Material> materials;
    void Awake()
    {
        Initialize();
    }

    public void Initialize ()
    {
        foreach (Transform child in transform)
        {
            Material material = materials[Random.Range(0, materials.Count)];

            child.GetComponent<Clothing>().Initialize(material);
        }
    }
}
