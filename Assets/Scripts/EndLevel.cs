using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour
{
    public int nextLevelIndex;
    public GameObject black;
    public GameObject endLevelInfo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            endLevelInfo.SetActive(true);
    }

    public void EndThisLevel()
    {
        black.SetActive(true);
        StartCoroutine("FadeToBlack");
        Invoke("LoadNextLevel", 1.2f);
    }

    IEnumerator FadeToBlack()
    {
        for (float i = 0; i <= 1; i += 0.05f)
        {
            Image img = black.GetComponent<Image>();
            Color c = img.color;
            c.a = i;
            img.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void LoadNextLevel()
    {
        UIMaster.Instance.ChangeScene(nextLevelIndex);
    }
}
