using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHealth.instance.HealPlayer(healAmount);
            AudioManager.instance.PlaySFX(5);
            Destroy(this.gameObject);
        }
    }
}
