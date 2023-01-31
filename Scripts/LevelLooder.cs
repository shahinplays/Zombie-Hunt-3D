using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLooder : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;
    public Text progressText;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    
    IEnumerator LoadAsynchronously(int scene)
    {
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = ((int)progress);
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }
}
