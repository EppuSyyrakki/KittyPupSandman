﻿using System.Collections;
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

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex > UIMaster.Instance.GetMainMenuSceneId() && Savepoint._isActive)
        {
            //Debug.Log("EventHandler invoked by update scene action");
            EventManager.RaiseOnUpdateScene();
        }       

        if (!Directory.Exists("C:/sandmanSaves"))
        {
            // Debug.Log("EventHandler invoked by create save file action");
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

        if (UIMaster.Instance.GetCurrentMenu() == "PauseMenu" ||
            UIMaster.Instance.GetCurrentMenu() == "OptionsMenu" ||
            UIMaster.Instance.GetCurrentMenu() == "ControlsMenu")
        {
            //Debug.Log("EventHandler invoked by pause action");
            if (!Pause._isPaused)
                EventManager.RaiseOnPause();
        }

        if (UIMaster.Instance.GetCurrentMenu() == "HudMenu" && UIMaster.Instance.GetCurrentSceneId() > UIMaster.Instance.GetMainMenuSceneId())
        {
            //Debug.Log("EventHandler invoked by resume action");
            if (Pause._isPaused)
                EventManager.RaiseOnResume();
        }

        if (EnemyController._isKillSound)
        {
            EventManager.RaiseOnEnemyDeath();
        }

        if (EnemyController._isAttackSoundGround)
        {
            EventManager.RaiseOnGroundEnemyAttack();
        }

        if (EnemyController._isAttackSoundAir)
        {
            EventManager.RaiseOnAirEnemyAttack();
        }

    }
}
