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
}
