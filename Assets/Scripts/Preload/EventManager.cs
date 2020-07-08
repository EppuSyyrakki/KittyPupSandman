using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnSaveGameDelegate();
    public static event OnSaveGameDelegate onSaveGameEvent;

    public delegate void OnSaveSceneDelegate();
    public static event OnSaveSceneDelegate onSaveSceneEvent;

    public static void RaiseOnSaveGame()
    {
        if (onSaveGameEvent != null)
            onSaveGameEvent();
    }

    public static void RaiseOnSaveScene()
    {
        if (onSaveSceneEvent != null)
            onSaveSceneEvent();
    }
}
