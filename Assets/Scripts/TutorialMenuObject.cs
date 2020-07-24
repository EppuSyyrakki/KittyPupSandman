using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenuObject : MonoBehaviour
{
    public int menuIndex;
    private EdgeCollider2D edgeCollider;

    public bool _isAlive;

    void Start()
    {
        _isAlive = true;
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            UIMaster.Instance._isPausedInTuto = true;
            UIMaster.Instance.ChangeMenu(menuIndex);
        }
    }

    private void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            Resume();
    }

    public void Resume()
    {
        UIMaster.Instance.ChangeMenu(0); // back to normal hud
        Destroy(edgeCollider);  // trigger this only once
        UIMaster.Instance._isPausedInTuto = false;
        _isAlive = false;
    }


}
