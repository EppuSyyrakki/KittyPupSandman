using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Diagnostics;
using TMPro;

public class SaveGame : MonoBehaviour
{
    private Vector2 playerPosVec;
    private String saveDataTxt;
    public static int sceneIndex;
    private String[] saveDataArr;
    public static bool _isInit { get; private set; }

    private bool _isContinueGame { get; set;  }

    public static SaveGame Instance { get; private set; }

    private void OnEnable()
    {
        EventManager.onSaveGameEvent += Save;
        EventManager.onUpdateSceneEvent += UpdateScene;
        EventManager.onNewSaveFileEvent += CreateNewSaveFile;
        EventManager.onReadFileEvent += ReadFile;
        EventManager.onContinueGameEvent += SetContinueGame;
    }

    private void OnDisable()
    {
        EventManager.onSaveGameEvent -= Save;
        EventManager.onUpdateSceneEvent -= UpdateScene;
        EventManager.onNewSaveFileEvent -= CreateNewSaveFile;
        EventManager.onReadFileEvent -= ReadFile;
        EventManager.onContinueGameEvent += SetContinueGame;
    }

    private void OnDestroy()
    {
        EventManager.onSaveGameEvent -= Save;
        EventManager.onUpdateSceneEvent -= UpdateScene;
        EventManager.onNewSaveFileEvent -= CreateNewSaveFile;
        EventManager.onReadFileEvent -= ReadFile; 
        EventManager.onContinueGameEvent += SetContinueGame;
    }

    private void SetContinueGame()
    {
        _isContinueGame = true;
    }

    private void Save()
    {
        CheckIsWriteFileAvailable(Savepoint.currentlyActivated);
    }

    private void UpdateScene()
    {
        if (!_isContinueGame)
        {
            sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            //UnityEngine.Debug.Log("Saving scene: " + sceneIndex);
        }
    }

    private void CreateNewSaveFile()
    {
        Directory.CreateDirectory("sandmanSaves/");
    }

    private void ReadFile()
    {
        _isInit = false;

        saveDataTxt = File.ReadAllText("sandmanSaves/" + transform.name + ".txt");

        //split the string into an array of strings at the separator written in our file

        saveDataArr = saveDataTxt.Split("!"[0]);

        // now change each of the strings back to numbers

        float.TryParse(saveDataArr[0], out playerPosVec.x);
        float.TryParse(saveDataArr[1], out playerPosVec.y);
        int.TryParse(saveDataArr[2], out sceneIndex);

        print("begin at pos:" + playerPosVec + " in scene " + sceneIndex);
    }

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            UnityEngine.Debug.LogWarning("Warning: multiple " + this + " in scene! (" + UIMaster.Instance.GetCurrentSceneId() + ")");
    }


    void Start()
    {
        _isInit = true;
    }

    public void WriteFile(Vector2 playerPos)
    {
        saveDataTxt = playerPos.x + "!" + playerPos.y;
        saveDataTxt = saveDataTxt + "!" + sceneIndex;

        File.WriteAllText("sandmanSaves/" + transform.name + ".txt", saveDataTxt);

        playerPosVec = playerPos;
        //UnityEngine.Debug.Log("Writing file...: " + saveDataTxt);
    }

    public void CheckIsWriteFileAvailable(Vector2 playerPos)
    {
        if (!_isContinueGame)
        {
            WriteFile(playerPos);
        }
        else if (_isContinueGame && playerPosVec != playerPos)
        {
            SetContinueGameStateFalse();
            WriteFile(playerPos);
        }
        else
        {
            UnityEngine.Debug.LogWarning("Not saving; current pos from memory");
        }
    }

    private void SetContinueGameStateFalse()
    {
        UIMaster.Instance._isContinue = false;
        _isContinueGame = false;
    }

    public int GetSceneIndex()
    {
        return sceneIndex;
    }

    public Vector2 GetPosFromMemory()
    {
        return playerPosVec;
    }
}