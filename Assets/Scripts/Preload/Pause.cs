using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool _isPaused { get; private set; }

    public static Pause Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            UnityEngine.Debug.LogWarning("Warning: multiple " + this + " in scene!");

        _isPaused = false;
    }

    private void OnEnable()
    {
        EventManager.onPauseEvent += PauseGame;
        EventManager.onResumeEvent += ResumeGame;
    }

    private void OnDisable()
    {
        EventManager.onPauseEvent -= PauseGame;
        EventManager.onResumeEvent -= ResumeGame;
    }

    private void OnDestroy()
    {
        EventManager.onPauseEvent -= PauseGame;
        EventManager.onResumeEvent -= ResumeGame;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;

        if ( !_isPaused)
            _isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        if (_isPaused)
            _isPaused = false;
    }
}
