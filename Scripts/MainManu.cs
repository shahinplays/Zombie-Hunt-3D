using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour
{
    public string sceneName;

    public void PlayButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }



    public void QuitButton()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }


    public void PausedButton()
    {
        UIManager.instance.GamePause();
    }


    public void ResumeButton()
    {
        UIManager.instance.GameUnPause();
    }


}
