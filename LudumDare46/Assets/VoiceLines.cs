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

    private int failPlays = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            _instance = this;
        }
            //Utility.PlayAudio(intro, this.gameObject);

    }

    public void PlayFail()
    {
        GetComponent<AudioSource>().Stop();
        if (failPlays <=1)
        {
            Utility.PlayAudio(fail, this.gameObject);
            failPlays++;
        }
        
    }

    public void PlayWin()
    {
        Utility.PlayAudio(Win, this.gameObject);
    }


}
