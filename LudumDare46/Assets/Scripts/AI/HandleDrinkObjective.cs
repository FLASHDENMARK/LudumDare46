using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDrinkObjective : ObjectiveBase
{
    public bool drinkSetActiveState = false;
    public GameObject drinksTohandle;
    public override Vector3 GetBeginningVector()
    {
        return drinksTohandle.transform.position;
    }

    public override Vector3 GetEndingVector()
    {
        throw new System.NotImplementedException();
    }

}
