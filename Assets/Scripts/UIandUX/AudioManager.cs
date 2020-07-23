using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    FMOD.Studio.EventInstance testSoundEvent;
    FMOD.Studio.PLAYBACK_STATE playbackState;

    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Master;

    float _musicVol = 0.5f;
    float _sfxVol = 0.5f;
    float _masterVol = 1f;

    void Awake()
    {
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");

        testSoundEvent = FMODUnity.RuntimeManager.CreateInstance("event:/monster/ballMonsterAggressiveRandom3D");
    }

    // Update is called once per frame
    void Update()
    {
        Music.setVolume(_musicVol);
        SFX.setVolume(_sfxVol);
        Master.setVolume(_masterVol);
    }

    public void MasterVolumeLevel(float newMasterVol)
    {
        _masterVol = newMasterVol;
        TestSound();
    }

    private void TestSound()
    {
        testSoundEvent.getPlaybackState(out playbackState);

        if (playbackState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            testSoundEvent.start();
            testSoundEvent.release();
        }
        else
            Debug.Log("it's playing already");
    }

    public void MusicVolumeLevel(float newMusicVol)
    {
        _musicVol = newMusicVol;
    }

    public void SFXVolumeLevel(float newSFXVol)
    {
        _sfxVol = newSFXVol;
    }
}
