using Assets.Scripts.Managers;
using System;
using UnityEngine;

public enum ObjectState { Off, On, Toggle }

public class HandleDrinkObjective : ObjectiveBase
{
    public ObjectState drinkState;
    public GameObject drinksTohandle;
    public AudioClip spawnSoundEffect;

    public override void Begin(Action<ObjectiveOutcome> endCallback)
    {
        base.Begin(endCallback);

        switch(drinkState)
        {
            case ObjectState.Off:
                drinksTohandle.SetActive(false);
                break;

            case ObjectState.On:
                drinksTohandle.SetActive(true);
                break;

            case ObjectState.Toggle:
                drinksTohandle.SetActive(!drinksTohandle.activeSelf);
                break;
        }

        _controller.GetComponent<AudioSource>().PlayOneShot(spawnSoundEffect);

        base.End();
    }

    protected override void UpdateObjective()
    {
        //base.UpdateObjective();


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
