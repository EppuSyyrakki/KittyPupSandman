using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Diagnostics;
using TMPro;

public class SaveGame : MonoBehaviour
{
    Vector2 vec;
    String txt;
    public static int sceneIndex;
    public String[] arr;

    public static SaveGame Instance { get; private set; }

    private void OnEnable()
    {
        EventManager.onSaveGameEvent += Save;
        EventManager.onSaveSceneEvent += SaveScene;
    }

    private void OnDisable()
    {
        EventManager.onSaveGameEvent -= Save;
        EventManager.onSaveSceneEvent -= SaveScene;
    }

    private void Save()
    {
        SetPosVec(Savepoint.currentlyActivated);
    }

    private void SaveScene()
    {
        sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        //UnityEngine.Debug.Log("Saving scene: " + sceneIndex);
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
        InitFromMemory();
    }

    private void InitFromMemory()
    {
        if (File.Exists("C:/sandmanSaves/" + transform.name + ".txt"))
        {
            txt = File.ReadAllText("C:/sandmanSaves/" + transform.name + ".txt");


            //split the string into an array of strings at the separator written in our file

            arr = txt.Split("!"[0]);

            // now change each of the strings back to numbers

            float.TryParse(arr[0], out vec.x);
            float.TryParse(arr[1], out vec.y);
            int.TryParse(arr[2], out sceneIndex);

            print("begin at pos:" + vec + " in scene " + sceneIndex);
        }
    }

    public void SetPosVec(Vector2 playerPos)
    {
        txt = playerPos.x + "!" + playerPos.y;
        txt = txt + "!" + sceneIndex;

        if (!Directory.Exists("C:/sandmanSaves")) 
        { 
            Directory.CreateDirectory("C:/sandmanSaves"); 
        }

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