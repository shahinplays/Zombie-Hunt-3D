using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public string theGun;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //add gun 
            PlayerControler.instance.AddGuns(theGun);
            AudioManager.instance.PlaySFX(4);
            Destroy(gameObject);
        }
    }


}
