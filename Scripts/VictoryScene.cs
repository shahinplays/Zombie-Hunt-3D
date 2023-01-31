using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScene : MonoBehaviour
{
    public GameObject textBox, ReturnButton;
    public Image blackScreen;
    private float fadeTime = 2f;

    void Start()
    {
        StartCoroutine(ShowObjectsCo());
    }

    void FixedUpdate()
    {
        blackScreen.color = new Color(0, 0, 0, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeTime * Time.fixedDeltaTime));
    }


    public void MainMenuScene()
    {
        SceneManager.LoadScene(0);
    }



    IEnumerator ShowObjectsCo()
    {
        yield return new WaitForSeconds(1.5f);
        textBox.SetActive(true);
        yield return new WaitForSeconds(1f);
        ReturnButton.SetActive(true);
    }

}
