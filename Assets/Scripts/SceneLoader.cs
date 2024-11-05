using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public GameObject loaderUI;
    public Slider progressBar;

    public void BackToMenu()
    {
        LoadScene(0);
    }

    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneCoroutine(index));
    }

    public IEnumerator LoadSceneCoroutine(int index)
    {
        progressBar.value = 0;
        loaderUI.SetActive(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.allowSceneActivation = false;

        float loadingProgress = 0;

        while (!asyncOperation.isDone)
        {
            loadingProgress = Mathf.MoveTowards(loadingProgress, asyncOperation.progress + 0.01f, Time.deltaTime);
            progressBar.value = loadingProgress;

            if (loadingProgress >= 0.9f)
            {
                progressBar.value = 1;
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
    
}
