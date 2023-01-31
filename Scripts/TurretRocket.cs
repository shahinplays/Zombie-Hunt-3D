using UnityEngine;

public class TurretRocket : MonoBehaviour
{
    public GameObject bullet;
    
    public float rangeToTargetPlayer = 10f, timeBetweenShots = 0.5f;
    public float rotateSpeed = 3f;
    private float shotCounter;

    public Transform gun, firePoint;
    public ParticleSystem muzzleFlash;

    void Start()
    {
        shotCounter = timeBetweenShots;
    }

    void LateUpdate()
    {
        if (PlayerControler.instance.gameObject.activeInHierarchy == false) { return; }

        if (Vector3.Distance(transform.position, PlayerControler.instance.transform.position) < rangeToTargetPlayer)
        {
            gun.LookAt(PlayerControler.instance.transform.position + new Vector3(0f, 1.2f, 0f));

            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                muzzleFlash.Play();
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
            }
        }
        else
        {
            shotCounter = timeBetweenShots;
            gun.rotation = Quaternion.Lerp(gun.rotation, Quaternion.Euler(0f, gun.eulerAngles.y + 10f, 0f), rotateSpeed * Time.deltaTime);
        }

        transform.rotation = Quaternion.LookRotation(transform.position - PlayerControler.instance.transform.position);
    }
}
