using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;
using System;
using System.Linq;

public class Notes : MonoBehaviour
{
    public TextMesh text;
    private List<Tuple<GameplayManager.IngameTime, string>> deaths;

    // Start is called before the first frame update
    void Start()
    {
        deaths = new List<Tuple<GameplayManager.IngameTime, string>>();
    }

    public void TargetDied(GameplayManager.IngameTime ingameTime, string cause) {
        if (deaths.Where(x => x.Item2 == cause).Count() == 0) {
            deaths.Add(new Tuple<GameplayManager.IngameTime, string>(ingameTime, cause));
            text.text += $"\n\nTime: {(ingameTime.hour < 10 ? "0" : "")}{ingameTime.hour}:{(ingameTime.minute < 10 ? "0" : "")}{ingameTime.minute}\nCause: {cause}";
        }
    }
}
