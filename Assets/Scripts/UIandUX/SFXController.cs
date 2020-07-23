using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    static FMOD.Studio.EventInstance EnemySound;

    private void OnEnable()
    {
        EventManager.onKillEnemySoundEvent += StopEnemySound;
        EventManager.onGroundEnemyAttackEvent += PlayGroundAttackSound;
        EventManager.onAirEnemyAttackEvent += PlayAirAttackSound;
    }

    private void OnDisable()
    {
        EventManager.onKillEnemySoundEvent -= StopEnemySound;
        EventManager.onGroundEnemyAttackEvent -= PlayGroundAttackSound;
        EventManager.onAirEnemyAttackEvent -= PlayAirAttackSound;
    }

    private void OnDestroy()
    {
        EventManager.onKillEnemySoundEvent -= StopEnemySound;
        EventManager.onGroundEnemyAttackEvent -= PlayGroundAttackSound;
        EventManager.onAirEnemyAttackEvent -= PlayAirAttackSound;
    }

    public void PlayMoveEnemy(string path)
    {
        EnemySound = FMODUnity.RuntimeManager.CreateInstance(path);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(EnemySound, transform, GetComponent<Rigidbody2D>());
        EnemySound.start();
        EnemySound.release();
    }

    private void PlayGroundAttackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/monster/ballMonsterAggressiveRandom3D");
    }

    private void PlayAirAttackSound()
    {
        //EnemySound.getVolume(out float vol);
        //EnemySound.setVolume(vol + 2.5f);
        Debug.LogWarning("No air attack sound available");
    }


    private void StopEnemySound()
    {
        EnemySound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    private void PlayFootstepsPlayer(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path);
    }

    private void PlayJumpPlayer(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path);
    }

}
