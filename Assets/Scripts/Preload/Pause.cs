using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool _isPaused { get; private set; }
    public static bool _okPause { get; set; }
    public static bool _okResume { get; set; }

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

        EventManager.onPauseTutorialEvent += PauseInTuto;
        EventManager.onResumeTutorialEvent += ResumeInTuto;
    }

    private void OnDisable()
    {
        EventManager.onPauseEvent -= PauseGame;
        EventManager.onResumeEvent -= ResumeGame;

        EventManager.onPauseTutorialEvent -= PauseInTuto;
        EventManager.onResumeTutorialEvent -= ResumeInTuto;
    }

    private void OnDestroy()
    {
        EventManager.onPauseEvent -= PauseGame;
        EventManager.onResumeEvent -= ResumeGame;

        EventManager.onPauseTutorialEvent -= PauseInTuto;
        EventManager.onResumeTutorialEvent -= ResumeInTuto;
    }

    public void PauseGame()
    {        

        if ( !_isPaused)
        {
            _isPaused = true; 
            Time.timeScale = 0f;
        }
    }

    private void PauseInTuto()
    {
        PauseGame();
    }

    public void ResumeGame()
    {
        if (_isPaused)
        {
            _isPaused = false;
            Time.timeScale = 1f;
        }
    }

    private void ResumeInTuto()
    {
        ResumeGame();
    }

    public float GetTime()
    {
        return Time.timeScale;
    }
}
