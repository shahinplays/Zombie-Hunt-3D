using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float zombieHealth = 100;
    public Slider healthSlider;

    public bool dropable;
    public GameObject[] pickables; 

    private void Start()
    {
        healthSlider.maxValue = zombieHealth;
        healthSlider.value = zombieHealth;
    }

    public void EnemyDamage(float damage)
    {
        zombieHealth -= damage;
        if (zombieHealth <= 0)
        {
            int dropRate = Random.Range(0, 10);
            if(dropRate >= 7 && dropable)
            {
                int dropItem = Random.Range(0, pickables.Length);
                Instantiate(pickables[dropItem], transform.position + new Vector3(0, 1f, 0), transform.rotation);
            }
       
            Destroy(gameObject); 
        }
        healthSlider.value = zombieHealth;
    }



}
