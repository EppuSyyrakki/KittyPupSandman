using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{

    private static FMOD.Studio.EventInstance backgroundMusic;


    private void OnDestroy()
    {
        backgroundMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }


    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic = FMODUnity.RuntimeManager.CreateInstance("event:/music/Soundscape1_level1");
        backgroundMusic.start();
        backgroundMusic.release();

    }
    
}
