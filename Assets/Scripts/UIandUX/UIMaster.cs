﻿using FMOD;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMaster : MonoBehaviour
{
    [SerializeField]
    private GameObject[] menus = null;

    [SerializeField]
    private int startMenu;

    public static GameObject currentMenu;
    private int currentMenuIndex;

    private int _preloadSceneId = 0; 
    private int _mainMenuSceneId = 1;
    private int _tutorialSceneId = 2;
    private int _firstSceneId = 3;

    public bool _isPausedInTuto { get; set; }
    public bool _isInTutoScene { get; set; }
    private bool _isNoGameMemory { get; set; }
    public bool _isContinue { get; set; }

    public static UIMaster Instance { get; private set; }

    private void OnEnable()
    {
        EventManager.onNewSaveFileEvent += InitNewGame;
        EventManager.onPlayerDeathEvent += ChangeDeathScene;
    }

    private void OnDisable()
    {
        EventManager.onNewSaveFileEvent -= InitNewGame;
        EventManager.onPlayerDeathEvent -= ChangeDeathScene;
    }

    private void OnDestroy()
    {
        EventManager.onNewSaveFileEvent -= InitNewGame;
        EventManager.onPlayerDeathEvent -= ChangeDeathScene;
    }

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            UnityEngine.Debug.LogWarning("Warning: multiple " + this + " in scene!");

    }
    private void Start()
    {
        if (menus != null)
        {
            currentMenuIndex = startMenu;
            currentMenu = menus[startMenu];
        }
        else
            UnityEngine.Debug.LogWarning("no menu to add");
    }
    private void InitNewGame()
    {
        WipeSfxPlayerSounds();
        _isNoGameMemory = true;
    }

    private static void WipeSfxPlayerSounds()
    {
        if (SFXPlayer.Instance._isHurt)
            SFXPlayer.Instance._isHurt = false;

        if (SFXPlayer.Instance._isDead)
            SFXPlayer.Instance._isDead = false;
    }

    public void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.N))
            StartNewGame();

        if (Input.GetKeyDown(KeyCode.C))
            ContinueGame();

        if (Input.GetKeyDown(KeyCode.M))
            ChangeScene(_mainMenuSceneId);

        if (Input.GetKeyDown(KeyCode.Q))
            QuitGame();
    }

    public void ChangeMenu(int menuIndex)
    {
        if (menus != null)
        {
            ValidChange(menuIndex);
        }
    }

    private void ValidChange(int menuIndex)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (i == menuIndex)
            {
                menus[i].SetActive(true);
                currentMenu = menus[i];
                currentMenuIndex = i;
                PauseAction(i);
            }
            else
                menus[i].SetActive(false);
        }
    }

    private static void PauseAction(int i)
    {
        if (i > 0)
        {
            Pause.Instance.PauseGame();
        }
        else
        {
            Pause.Instance.ResumeGame();
        }
    }

    public void ChangeScene(int sceneID)
    {
        CheckIfInTutoScene(sceneID);
        
        // this method takes index values from build settings
        SceneManager.LoadScene(sceneBuildIndex: sceneID);
    }

    private void CheckIfInTutoScene(int sceneID)
    {
        if (sceneID != _tutorialSceneId)
            _isInTutoScene = false;
        else
            _isInTutoScene = true;
    }

    public void StartNewGame()
    {
        ChangeScene(_tutorialSceneId);
        WipeSfxPlayerSounds();
        HitCheckSetToDefault();
        SaveGame.Instance.CheckIsWriteFileAvailable(new Vector2(0, 0));     // clear player pos from the memory
    }

    private static void HitCheckSetToDefault()
    {
        HitCheck.Instance.SetIsHit();
        HitCheck.Instance.SetIsDamage();
    }

    public void ContinueGame()
    {
        int i = SaveGame.Instance.GetSceneIndex();
        CheckIfMemoryExists(i);
        HitCheckSetToDefault();
        setMenuToDefault();

    }

    private void setMenuToDefault()
    {
        if (currentMenuIndex != startMenu)
        {
            currentMenuIndex = startMenu;
            currentMenu = menus[startMenu];
        }
    }

    private void CheckIfMemoryExists(int i)
    {
        if (_isNoGameMemory)
        {
            UnityEngine.Debug.Log("No previous memory, initiating new game!");
            StartNewGame();
        }
        else
        {
            _isContinue = true;
            ChangeScene(i);
        }
    }

    public void EnterMainMenuScene()
    {
        ChangeScene(_mainMenuSceneId);

        //Debug.LogWarning("Main menu, scene index in UI: " + SceneManager.GetActiveScene().buildIndex);
    }

    private void ChangeDeathScene()
    {
        ChangeScene(4);
        PlayerInputController.Instance._isDie = false;
    }

    public int GetCurrentSceneId()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public int GetMainMenuSceneId()
    {
        return _mainMenuSceneId;
    }

    public int GetFirstLevelSceneId()
    {
        return _firstSceneId;
    }

    public void QuitGame()
    {
        /*
        These lines starting with # are called directives and they can
        be used to have code that only works in specific environments,
        like in the editor here

        More reading:
        https://docs.unity3d.com/Manual/PlatformDependentCompilation.html
        https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/preprocessor-directives/preprocessor-if
        */
#if UNITY_EDITOR
        // This doesn't technically have much reason to exist
        // because you can always exit play mode manually but
        // it can help with testing
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        // This is a graceful way to quit games, better than Alt+F4
        Application.Quit();
#endif
    }
}
