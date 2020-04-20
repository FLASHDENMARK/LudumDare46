using Assets.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLines : MonoBehaviour
{
    public static VoiceLines _instance;

    public AudioClip intro;
    public AudioClip fail;
    public AudioClip Win;

    

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        _instance = this;
        Utility.PlayAudio(intro, this.gameObject);
    }

    public void PlayFail()
    {
        Utility.PlayAudio(fail, this.gameObject);
    }

    public void PlayWin()
    {
        Utility.PlayAudio(Win, this.gameObject);
    }


}
