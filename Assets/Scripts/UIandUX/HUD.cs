using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI saveText = null;

    [SerializeField]
    private string noMessage = "";

    [SerializeField]
    private string message = "Game Saved!";

    private void OnEnable()
    {
        EventManager.onSaveGameEvent += popMessage;
        EventManager.onCloseMessageEvent += closeMessage;
    }

    private void OnDisable()
    {
        EventManager.onSaveGameEvent -= popMessage;
        EventManager.onCloseMessageEvent -= closeMessage;
    }

    private void OnDestroy()
    {
        EventManager.onSaveGameEvent -= popMessage;
        EventManager.onCloseMessageEvent -= closeMessage;
    }

    private void popMessage()
    {
        if (saveText)
            saveText.text = message;        
    }

    private void closeMessage()
    {
        if (saveText)
        {
            saveText.text = noMessage;
        }
    }
   
}
