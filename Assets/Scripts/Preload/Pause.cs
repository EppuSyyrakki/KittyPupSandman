using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool _isPaused { get; private set; }

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

    void Awake()
    {
        _isPaused = false;
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;

        if ( !_isPaused)
            _isPaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;

        if (_isPaused)
            _isPaused = false;
    }
}
