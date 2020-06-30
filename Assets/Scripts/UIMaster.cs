using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMaster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ChangeScene(2);
        }

        if (Input.GetKeyDown(KeyCode.M))
        { 
            ChangeScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitGame();
        }
    }

    public void ChangeScene(int sceneID)
    {
        // this method takes index values from build settings
        SceneManager.LoadScene(sceneBuildIndex: sceneID);
    }

    public void QuitGame()
    {
        /*
        These lines starting with # are called directives and they can
        be used to have code that only works in specific environments,
        like in the editor here

        More reading:
        https://docs.unity3d.com/Manual/PlatformDependentCompilation.html
        https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/preprocessor-directives/preprocessor-if
        */
#if UNITY_EDITOR
        // This doesn't technically have much reason to exist
        // because you can always exit play mode manually but
        // it can help with testing
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        // This is a graceful way to quit games, better than Alt+F4
        Application.Quit();
#endif
    }
}
