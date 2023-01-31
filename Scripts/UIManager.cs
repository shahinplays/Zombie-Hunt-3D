using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Slider healthSlider;
    public Text healthText;
    public Text ammoText;
    public Image currentGunImage;

    // player damage show 
    public Image damageImage;
    public float damageAlpha = 0.25f, damageFadeSpeed = 1f;

    public GameObject pausedPanal;
    public GameObject gameOverPanal;
    public GameObject pauseButton;
    public GameObject mobileControlPanal;

    public Image blackScreen;
    public float fadeSpeed = 1.5f;

    private InterstitialAd interstitialAd;

    void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        interstitialAd = FindObjectOfType<InterstitialAd>();
    }

    void Update()
    {
        if (damageImage.color.a != 0)
        {
            damageImage.color = new Color(1f, 0f, 0f, Mathf.MoveTowards(damageImage.color.a, 0f, damageFadeSpeed * Time.deltaTime));
        }

        if (!GameManager.instance.levelEnding)
        {
            blackScreen.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
        }
        else
        {
            blackScreen.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
        }


    }


    public void ShowDamage()
    {
        damageImage.color = new Color(1f, 0f, 0f, 0.5f);
    }





    public void GameUnPause()
    {
        pausedPanal.SetActive(false);
        mobileControlPanal.SetActive(true);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }


    public void GamePause()
    {
        pausedPanal.SetActive(true);
        mobileControlPanal.SetActive(false);
        pauseButton.SetActive(false);
        StartCoroutine(WaitForPause());
    }


    IEnumerator WaitForPause()
    {
        yield return new WaitForSeconds(0.7f);
        Time.timeScale = 0;
        interstitialAd.ShowAd();
        
    }









}
