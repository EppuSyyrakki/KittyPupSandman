using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private TutorialMenuObject[] tutoMenus;
    public GameObject black;

    private int currentIndex = 0;
    private TutorialMenuObject currentObj;
   

    private void Update()
    {
        ActivateCurrentObject();
        SetCurrentIndex();
        RunEndGame();

    }

    private void RunEndGame()
    {
        if (currentObj.name == "End" && !currentObj._isAlive)
            EndTutorial();
    }

    private void SetCurrentIndex()
    {
        if (!currentObj._isAlive)
        {
            currentIndex++;
        }
    }

    private void ActivateCurrentObject()
    {
        for (int i = 0; i < tutoMenus.Length; i++)
        {
            if (i == currentIndex)
            {
                tutoMenus[i].gameObject.SetActive(true);
                currentObj = tutoMenus[i];
            }
            else
            {
                tutoMenus[i].gameObject.SetActive(false);
            }
        }
    }

    public void EndTutorial()
    {
        black.SetActive(true);
        StartCoroutine("FadeToBlack");
        Invoke("LoadNextLevel", 1.2f);
        print(name + " ends tutorial");
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
        UIMaster.Instance.ChangeScene(UIMaster.Instance.GetCurrentSceneId() + 1);
    }
}