using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnSaveGame();
    public static event OnSaveGame onSaveGameEvent;

    public delegate void OnUpdateScene();
    public static event OnUpdateScene onUpdateSceneEvent;

    public delegate void OnNewSaveFile();
    public static event OnNewSaveFile onNewSaveFileEvent;

    public delegate void OnReadFile();
    public static event OnReadFile onReadFileEvent;

    public delegate void OnCloseMessage();
    public static event OnCloseMessage onCloseMessageEvent;

    public delegate void OnPauseGame();
    public static event OnPauseGame onPauseEvent;

    public delegate void OnResumeGame();
    public static event OnResumeGame onResumeEvent;

    public delegate void OnDeadEnemy();
    public static event OnDeadEnemy onKillEnemySoundEvent;

    public delegate void OnAttackingGroundEnemy();
    public static event OnAttackingGroundEnemy onGroundEnemyAttackEvent;

    public delegate void OnAttackingAirEnemy();
    public static event OnAttackingAirEnemy onAirEnemyAttackEvent;


    public static void RaiseOnSaveGame()
    {
        if (onSaveGameEvent != null)
            onSaveGameEvent();
    }

    public static void RaiseOnUpdateScene()
    {
        if (onUpdateSceneEvent != null)
            onUpdateSceneEvent();
    }

    public static void RaiseOnNewSaveFile() 
    {
        if (onNewSaveFileEvent != null)
            onNewSaveFileEvent();
    }

    public static void RaiseOnReadFile()
    {
        if (onReadFileEvent != null)
            onReadFileEvent();
    }

    public static void RaiseOnCloseMessage()
    {
        if (onCloseMessageEvent != null)
            onCloseMessageEvent();
    }

    public static void RaiseOnPause()
    {
        if (onPauseEvent != null)
            onPauseEvent();
    }

    public static void RaiseOnResume()
    {
        if (onResumeEvent != null)
            onResumeEvent();
    }

    public static void RaiseOnEnemyDeath()
    {
        //Debug.Log("Enemy dies");
        if (onKillEnemySoundEvent != null)
            onKillEnemySoundEvent();
    }

    public static void RaiseOnGroundEnemyAttack()
    {
        //Debug.Log("Enemy attaaaacks!");
        if (onGroundEnemyAttackEvent != null)
            onGroundEnemyAttackEvent();
    }

    public static void RaiseOnAirEnemyAttack()
    {
        //Debug.Log("Air enemy attaaaaacks!");
        if (onAirEnemyAttackEvent != null)
            onAirEnemyAttackEvent();
    }

}
