using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //give ammo
            PlayerControler.instance.acticeGun.GetActiveGunAmmo();
            AudioManager.instance.PlaySFX(3);
            Destroy(gameObject);
        }
    }
}
