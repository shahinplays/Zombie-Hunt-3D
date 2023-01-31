using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectile;
    public Transform firePoint;


    public float fireRate;
    [HideInInspector] public float fireCounter;
    
    public int currentAmmo, pickUpAmount;
    public float zoomAmount;
    public string gunName;
    public Sprite gunImage;

    public bool isSniper;
    public GameObject scopeImage,gunModle;

    void Update()
    {
        if (fireCounter > 0)
        {
            fireCounter -= Time.deltaTime;
        }
    }


    public void GetActiveGunAmmo()
    {
        currentAmmo += pickUpAmount;
        UIManager.instance.ammoText.text = "AMMO : " + currentAmmo;
    }






}
