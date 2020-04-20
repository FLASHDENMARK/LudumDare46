using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;
using System;
using System.Linq;

public class Notes : MonoBehaviour
{
    public NotesSO NotesSO;
    public TextMesh text;
    private List<Tuple<GameplayManager.IngameTime, string>> deaths;

    // Start is called before the first frame update
    void Start()
    {
        deaths = NotesSO.deaths;

        foreach (var note in deaths)
        {
            text.text += $"\n\nTime: {(note.Item1.hour < 10 ? "0" : "")}{note.Item1.hour}:{(note.Item1.minute < 10 ? "0" : "")}{note.Item1.minute}\nCause: {note.Item2}";
        }
        this.gameObject.SetActive(false);
    }

    public void TargetDied(GameplayManager.IngameTime ingameTime, string cause) {
        if (deaths.Where(x => x.Item2 == cause).Count() == 0) {
            NotesSO.deaths.Add(new Tuple<GameplayManager.IngameTime, string>(ingameTime, cause));
            text.text += $"\n\nTime: {(ingameTime.hour < 10 ? "0" : "")}{ingameTime.hour}:{(ingameTime.minute < 10 ? "0" : "")}{ingameTime.minute}\nCause: {cause}";
        }
    }
}
