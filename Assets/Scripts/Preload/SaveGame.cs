using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Diagnostics;
using TMPro;

public class SaveGame : MonoBehaviour
{
    private Vector2 vec;
    private String txt;
    public static int sceneIndex;
    private String[] saveDataArr;
    public static bool _isInit { get; private set; }

    public static SaveGame Instance { get; private set; }

    private void OnEnable()
    {
        EventManager.onSaveGameEvent += Save;
        EventManager.onUpdateSceneEvent += UpdateScene;
        EventManager.onNewSaveFileEvent += CreateNewSaveFile;
        EventManager.onReadFileEvent += ReadFile;
    }

    private void OnDisable()
    {
        EventManager.onSaveGameEvent -= Save;
        EventManager.onUpdateSceneEvent -= UpdateScene;
        EventManager.onNewSaveFileEvent -= CreateNewSaveFile;
        EventManager.onReadFileEvent -= ReadFile;
    }

    private void OnDestroy()
    {
        EventManager.onSaveGameEvent -= Save;
        EventManager.onUpdateSceneEvent -= UpdateScene;
        EventManager.onNewSaveFileEvent -= CreateNewSaveFile;
        EventManager.onReadFileEvent -= ReadFile;
    }

    private void Save()
    {
        SetPosVec(Savepoint.currentlyActivated);
    }

    private void UpdateScene()
    {
        sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        //UnityEngine.Debug.Log("Saving scene: " + sceneIndex);
    }

    private void CreateNewSaveFile()
    {
        Directory.CreateDirectory("C:/sandmanSaves");
    }

    private void ReadFile()
    {
        _isInit = false;

        txt = File.ReadAllText("C:/sandmanSaves/" + transform.name + ".txt");

        //split the string into an array of strings at the separator written in our file

        saveDataArr = txt.Split("!"[0]);

        // now change each of the strings back to numbers

        float.TryParse(saveDataArr[0], out vec.x);
        float.TryParse(saveDataArr[1], out vec.y);
        int.TryParse(saveDataArr[2], out sceneIndex);

        print("begin at pos:" + vec + " in scene " + sceneIndex);
    }

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            UnityEngine.Debug.LogWarning("Warning: multiple " + this + " in scene!");
    }


    void Start()
    {
        _isInit = true;
    }

    public void SetPosVec(Vector2 playerPos)
    {
        txt = playerPos.x + "!" + playerPos.y;
        txt = txt + "!" + sceneIndex;

        File.WriteAllText("C:/sandmanSaves/" + transform.name + ".txt", txt);

        vec = playerPos;
    }

    public int GetSceneIndex()
    {
        return sceneIndex;
    }

    public Vector2 GetPosFromMemory()
    {
        return vec;
    }
}