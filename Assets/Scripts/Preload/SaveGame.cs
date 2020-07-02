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
    public int sceneIndex;
    public String[] arr;

    public static SaveGame Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            UnityEngine.Debug.LogWarning("Warning: multiple " + this + " in scene!");
    }

    void Start()
    {

        if (File.Exists("C:/sandmanSaves/" + transform.name + ".txt"))
        {
            txt = File.ReadAllText("C:/sandmanSaves/" + transform.name + ".txt");

            //print("got this: " + txt);

            //split the string into an array of strings at the separator written in our file

            arr = txt.Split("!"[0]);

            // now change each of the strings back to numbers

            float.TryParse(arr[0], out vec.x);
            float.TryParse(arr[1], out vec.y);
            int.TryParse(arr[2], out sceneIndex);
            //move our character to the position we got from our file

            print("begin at pos:" + vec);
            print("and at scene: " + sceneIndex);
            //UIMaster.Instance.ChangeScene(sceneIndex);
        }

    }

    void Update()
    {
    }

    public void SetPosVec(Vector2 playerPos)
    {
        SetSceneIndex();
        txt = playerPos.x + "!" + playerPos.y;
        txt = txt + "!" + sceneIndex;

        if (!Directory.Exists("C:/sandmanSaves")) { Directory.CreateDirectory("C:/sandmanSaves"); }
        File.WriteAllText("C:/sandmanSaves/" + transform.name + ".txt", txt);

        vec = playerPos;
        
       //print("saving: " + playerPos + " in scene: " + sceneIndex);
    }

    private void SetSceneIndex()
    {
        int tmp = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        if (tmp >= sceneIndex) // check that the active scene is a game scene (not menu nor preload)
            sceneIndex = tmp;

        //print("active scene in save game: " + sceneIndex);
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