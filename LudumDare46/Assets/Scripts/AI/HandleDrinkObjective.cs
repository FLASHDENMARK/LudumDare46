using Assets.Scripts.Managers;
using System;
using UnityEngine;

public enum ObjectState { Off, On, Toggle }

public class HandleDrinkObjective : ObjectiveBase
{
    public ObjectState drinkState;
    public GameObject drinksTohandle;
    public AudioClip spawnSoundEffect;
    public ObjectiveBase prerequisite;

    public override void Begin(Action<ObjectiveOutcome> endCallback)
    {
        if (prerequisite != null && !prerequisite.wasSuccesful)
        {
            base.End(false);
        }

        base.Begin(endCallback);

        HandleDrink(drinksTohandle);

        base.End();
    }

    public void HandleDrink (GameObject obj)
    {
        switch (drinkState)
        {
            case ObjectState.Off:
                obj.SetActive(false);
                break;

            case ObjectState.On:
                obj.SetActive(true);
                break;

            case ObjectState.Toggle:
                obj.SetActive(!drinksTohandle.activeSelf);
                break;
        }

        _controller.GetComponent<AudioSource>().PlayOneShot(spawnSoundEffect);
    }

    public override Vector3 GetBeginningVector()
    {
        return drinksTohandle.transform.position;
    }

    public override Vector3 GetEndingVector()
    {
        return drinksTohandle.transform.position;
    }
}
