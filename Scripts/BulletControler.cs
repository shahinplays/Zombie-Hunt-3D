using UnityEngine;

public class BulletControler : MonoBehaviour
{
    public float speed = 15f;
    public float damageAmount;
    public float hitOffset = 0f;
    public GameObject hit;
    public GameObject flash;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        if (flash != null)
        {
            var flashInstance = Instantiate(flash, transform.position, transform.rotation);
            flashInstance.transform.forward = gameObject.transform.forward;
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
        Destroy(gameObject, 5);
    }



    //https ://docs.unity3d.com/ScriptReference/Rigidbody.OnCollisionEnter.html
    void OnCollisionEnter(Collision collision)
    {
        //Lock all axes movement and rotation
        rb.constraints = RigidbodyConstraints.FreezeAll;
        speed = 0;

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point + contact.normal * hitOffset;
           

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.instance.PlayerDamage(damageAmount);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null) { enemyHealth.EnemyDamage(damageAmount); }
        }


        var hitInstance = Instantiate(hit, pos, rot);
        Destroy(hitInstance, 1f);

        Destroy(gameObject);
    }


}
