using System.Collections;
using System.Collections.Generic;
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
            EventManager.RaiseOnSaveScene();
        }
            
    }
}
