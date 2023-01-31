using UnityEngine;

public class RocketControler : MonoBehaviour
{
    public int damage = 1;
    public float speed, lifeTime;
    public GameObject rocketImpact;
    public Rigidbody rb;

    void Awake()
    {
        rb.velocity = transform.forward * speed;

        Destroy(gameObject, lifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth.instance.PlayerDamage(damage);
        }

        GameObject rocketBlast = Instantiate(rocketImpact, transform.position, transform.rotation);
        Destroy(rocketBlast, 1f);
        AudioManager.instance.PlaySFX(2);
        Destroy(gameObject);
    }


    
}
