using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadSceneTest : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public bool canGoToScene = false;

    AsyncOperation asyncOperation;

    // Start is called before the first frame update
    void Start()
    {
        //Start loading the Scene asynchronously and output the progress bar
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;

        //Begin to load the Scene you specify
        asyncOperation = SceneManager.LoadSceneAsync(1);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Progress :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            buttonText.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                
                //Change the Text to show the Scene is ready
                buttonText.text = "Press to continue";
                while(!canGoToScene) {
                    yield return null;
                }

                if(canGoToScene) asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void GoToScene() {
        canGoToScene = true;
    }
}
