using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string nextLevel;
    public float waitToEndLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.levelEnding = true;
            StartCoroutine(EndLevelCo());
            AudioManager.instance.PlayLevelVictory();
        }
    }



    IEnumerator EndLevelCo()
    {
        yield return new WaitForSeconds(waitToEndLevel);
        SceneManager.LoadScene(nextLevel);
    }






}
