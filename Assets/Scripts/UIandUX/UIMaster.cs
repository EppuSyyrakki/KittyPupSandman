using FMOD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMaster : MonoBehaviour
{
    [SerializeField]
    private GameObject[] menus = null;

    [SerializeField]
    private int startMenu = 0;

    public static GameObject currentMenu;

    private int _preloadSceneId = 0; 
    private int _mainMenuSceneId = 1;
    private int _firstLevelSceneId = 2;

    public bool _isPausedInTuto;

    public static UIMaster Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            UnityEngine.Debug.LogWarning("Warning: multiple " + this + " in scene!");

    }
    private void Start()
    {
        currentMenu = menus[startMenu];

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

        if (Input.GetKeyDown(KeyCode.Return))
        {
            
        }
    }

    public void ChangeMenu(int menuIndex)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (i == menuIndex)
            {
                menus[i].SetActive(true);
                currentMenu = menus[i];
                //Debug.Log("Menu changed to: " + currentMenu.tag);
            }
            else
                menus[i].SetActive(false);
        }
    }

    public void ChangeScene(int sceneID)
    {

        // this method takes index values from build settings
        SceneManager.LoadScene(sceneBuildIndex: sceneID);
    }

    public void StartNewGame()
    {
        ChangeScene(_firstLevelSceneId);
        SaveGame.Instance.WriteFile(new Vector2(0, 0));     // clear player pos from the memory
    }

    public void ContinueGame()
    {
        int i = SaveGame.Instance.GetSceneIndex();
        //Debug.LogWarning("Continue game, scene index in UI: " + i);

        if (i == _preloadSceneId)
            StartNewGame(); 
        else
            ChangeScene(i);
    }

    public void EnterMainMenuScene()
    {
        ChangeScene(_mainMenuSceneId);

        //Debug.LogWarning("Main menu, scene index in UI: " + SceneManager.GetActiveScene().buildIndex);
    }

    public string GetCurrentMenu()
    {
        return currentMenu.tag;
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
        return _firstLevelSceneId;
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
