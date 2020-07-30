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

        if (Savepoint._isActive && !UIMaster.Instance._isContinue)
        {
            //Debug.Log("EventHandler invoked by savepoint action");
            EventManager.RaiseOnSaveGame();
            timer = 0;
        }

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex > UIMaster.Instance.GetMainMenuSceneId() && Savepoint._isActive)
        {
            //Debug.Log("EventHandler invoked by update scene action");
            EventManager.RaiseOnUpdateScene();
        }       

        if (!Directory.Exists("sandmanSaves"))
        {
            //Debug.Log("EventHandler invoked by create save file action");
            EventManager.RaiseOnNewSaveFile();
        }

        if (File.Exists("sandmanSaves/" + transform.name + ".txt") && SaveGame._isInit)
        {
            //Debug.Log("EventHandler invoked by read file action");
            EventManager.RaiseOnReadFile();
        }

        if (UIMaster.Instance._isContinue)
        {
            EventManager.RaiseOnContinueGame();
        }

        if (!Savepoint._isActive && timer > 1.5f)
        {
            EventManager.RaiseOnCloseMessage();
            timer = 0;
        }

        if (UIMaster.Instance._isInTutoScene && UIMaster.Instance._isPausedInTuto)
        {
            EventManager.RaiseOnPauseInTutorial();
        }

        if (UIMaster.Instance._isInTutoScene && !UIMaster.Instance._isPausedInTuto)
        {
            EventManager.RaiseOnResumeTutorial();
        }

        if (HitCheck._playerDamage)
        {
            EventManager.RaiseOnPlayerDamage();
        }

        if (PlayerInputController.Instance._isDie)
        {
            EventManager.RaiseOnPlayerDeath();
        }

    }
}
