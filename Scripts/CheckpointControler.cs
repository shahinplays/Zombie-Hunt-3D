using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointControler : MonoBehaviour
{
    public string cpName;

    void Start()
    {
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_cp"))
        {
            if (PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "_cp") == cpName)
            {
                PlayerControler.instance.transform.position = transform.position;
                print("Player Starting at " + cpName);
            }
        } 
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", cpName);
            print("Player Hit " + cpName);
            AudioManager.instance.PlaySFX(1);
        }
    }



}
