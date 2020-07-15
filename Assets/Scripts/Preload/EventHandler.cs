using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Savepoint._isActive)
        {
            //Debug.Log("EventHandler invoked by savepoint action");
            EventManager.RaiseOnSaveGame();
            timer = 0;
        }

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex >= 3 && Savepoint._isActive)
        {
            //Debug.Log("EventHandler invoked by save scene action");
            EventManager.RaiseOnUpdateScene();
        }       

        if (!Directory.Exists("C:/sandmanSaves"))
        {
            //Debug.Log("EventHandler invoked by create save file action");
            EventManager.RaiseOnNewSaveFile();
        }

        if (File.Exists("C:/sandmanSaves/" + transform.name + ".txt") && SaveGame._isInit)
        {
            //Debug.Log("EventHandler invoked by read file action");
            EventManager.RaiseOnReadFile();
        }

        if (!Savepoint._isActive && timer > 1.5f)
        {
            EventManager.RaiseOnCloseMessage();
            timer = 0;
        }

    }
}
