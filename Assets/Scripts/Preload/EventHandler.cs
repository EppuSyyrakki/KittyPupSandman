using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EventHandler : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Savepoint._isActive)
        {
            //Debug.Log("EventHandler invoked on savepoint action");
            EventManager.RaiseOnSaveGame();
        }

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex >= 2 && Savepoint._isActive)
        {
            //Debug.Log("EventHandler invoked on save scene action");
            EventManager.RaiseOnUpdateScene();
        }       

        if (!Directory.Exists("C:/sandmanSaves"))
        {
            Debug.Log("EventHandler invoked on create save file action");
            EventManager.RaiseOnNewSaveFile();
        }

        if (File.Exists("C:/sandmanSaves/" + transform.name + ".txt") && SaveGame._isInit)
        {
            Debug.Log("EventHandler invoked on read file action");
            EventManager.RaiseOnReadFile();
        }

    }
}
