using Assets.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NotesSO", menuName = "NotesSO")]
public class NotesSO : ScriptableObject
{
    public List<Tuple<GameplayManager.IngameTime, string>> deaths = new List<Tuple<GameplayManager.IngameTime, string>>();
}
