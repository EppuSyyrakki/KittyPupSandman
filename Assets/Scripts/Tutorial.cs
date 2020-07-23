using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Canvas canvas;
    public int menuIndex;
    public GameObject black;
    private EdgeCollider2D edgeCollider;
    private UIMaster uiMaster;

    void Start()
    {
        uiMaster = canvas.GetComponent<UIMaster>();
        edgeCollider = GetComponent<EdgeCollider2D>();        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Time.timeScale = 0f;    // purukumiratkaisu - TODO event
            uiMaster.ChangeMenu(menuIndex);
        }       
    }

    public void Resume()
    {
        uiMaster.ChangeMenu(0); // back to normal hud
        Destroy(edgeCollider);  // trigger this only once
        Time.timeScale = 1f;    // purukumi - TODO event
    }

    public void EndTutorial()
    {
        Resume();
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
        uiMaster.ChangeScene(uiMaster.GetCurrentSceneId() + 1);
    }
}
