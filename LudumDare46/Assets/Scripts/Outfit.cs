using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An outfit represents a collection of different clothing
/// </summary>
public class Outfit : MonoBehaviour
{
    public List<Material> testMat;
    void Awake()
    {
        Initialize();
    }

    public void Initialize ()
    {
        foreach (Transform child in transform)
        {
            Material material = testMat[Random.Range(0, testMat.Count)];

            child.GetComponent<Clothing>().Initialize(material);
        }
    }
}
