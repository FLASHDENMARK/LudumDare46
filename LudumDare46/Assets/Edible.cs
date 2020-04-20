using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : MonoBehaviour, IDamageGiver
{

    public bool IsPoisonous;

    public ControllerBase DamageGiver => null;

    public string CauseOfDamage => "Poison";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
