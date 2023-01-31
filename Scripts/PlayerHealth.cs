using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public float maxHealth = 10;
    private float currentHealth;

    public float invincibleLength = 0.5f;
    private float invincCounter;


    private InterstitialAd interstitialAd;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;

        UIManager.instance.healthSlider.maxValue = maxHealth;
        UIManager.instance.healthSlider.value = currentHealth;

        UIManager.instance.healthText.text = currentHealth + "/" + maxHealth;
        
        interstitialAd = FindObjectOfType<InterstitialAd>();
    }


    private void Update()
    {
        if (invincCounter > 0) 
        {
            invincCounter -= Time.deltaTime;
        }
    }

    public void PlayerDamage(float damage)
    {
        if (invincCounter <= 0 && !GameManager.instance.levelEnding)
        {
            currentHealth -= damage;
            AudioManager.instance.PlaySFX(7);
            UIManager.instance.ShowDamage();

            UIManager.instance.healthSlider.value = currentHealth;
            UIManager.instance.healthText.text = currentHealth + "/" + maxHealth;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                UIManager.instance.gameOverPanal.SetActive(true);
                UIManager.instance.mobileControlPanal.SetActive(false);
                UIManager.instance.pauseButton.SetActive(false);
                AudioManager.instance.PlaySFX(6);
                gameObject.SetActive(false);

                interstitialAd.ShowAd();
            }

            invincCounter = invincibleLength;
        }
    }




    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIManager.instance.healthSlider.value = currentHealth;
        UIManager.instance.healthText.text = currentHealth + "/" + maxHealth;
    }





}
