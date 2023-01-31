using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool levelEnding;
    [SerializeField] GameObject levelExit;
    public GameObject[] enemies;

    private void Awake()
    {
        instance = this;
    }



    void LateUpdate()
    {

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length <= 0) { levelExit.SetActive(true); }
        else { levelExit.SetActive(false); }

    }







}
