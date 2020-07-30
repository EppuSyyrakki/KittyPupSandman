using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public void GoToNextLevel()
    {
        GameObject matti = GameObject.FindGameObjectWithTag("Player");
        matti.transform.position = Vector3.zero;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }
}
