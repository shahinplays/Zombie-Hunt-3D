using UnityEngine;
using UnityEngine.AI;

public class EnemyControler : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public float moveSpeed;
    public float distanceToChase = 10f, distanceToLose = 15f, distanceToStop = 2f;
    private bool chasing;
    
    private Vector3 targetPoint, startPoint;
    
    public float keepChasigTime = 5f;
    private float chaseCounter;

    // enemy fire
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate = 0.5f, waitBetweenShots = 2f, timeToShoot = 1f;
    private float fireCount, shotWaitCounter, shootTimeCounter;

    public Animator anim;


    void Start()
    {
        startPoint = transform.position;

        shootTimeCounter = timeToShoot;
        shotWaitCounter = waitBetweenShots;
    }


    void Update()
    {
        EnemyChaseAndShoot();


    }


    private void EnemyChaseAndShoot()
    {
        targetPoint = PlayerControler.instance.transform.position;


        if (!chasing)
        {
            if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                chasing = true;

                shootTimeCounter = timeToShoot;
                shotWaitCounter = waitBetweenShots;
            }

            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;
                if (chaseCounter <= 0)
                {
                    navAgent.destination = startPoint;
                }
            }

            if (navAgent.remainingDistance < 1f)
            {
                anim.SetBool("isMove", false);
            }
            else { anim.SetBool("isMove", true); }
        }
        else
        {
            if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
            {
                navAgent.destination = targetPoint;
            }
            else
            {
                navAgent.destination = transform.position;
            }

            if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)
            {
                chasing = false;
                chaseCounter = keepChasigTime;
            }

            //enemy fire
            if (shotWaitCounter > 0)
            {
                shotWaitCounter -= Time.deltaTime;

                if (shotWaitCounter <= 0)
                {
                    shootTimeCounter = timeToShoot;
                }

                anim.SetBool("isMove", true);
            }
            else
            {
                if (PlayerControler.instance.gameObject.activeInHierarchy)
                {
                    shootTimeCounter -= Time.deltaTime;

                    if (shootTimeCounter > 0)
                    {
                        fireCount -= Time.deltaTime;

                        if (fireCount <= 0)
                        {
                            fireCount = fireRate;

                            firePoint.LookAt(targetPoint + new Vector3(0f, 1f, 0f));

                            //check the angle to the player
                            Vector3 targetDir = targetPoint - transform.position;
                            float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

                            if (Mathf.Abs(angle) < 30f)
                            {
                                Instantiate(bullet, firePoint.position, firePoint.rotation);
                                anim.SetTrigger("fireShot");
                            }
                            else
                            {
                                shotWaitCounter = waitBetweenShots;
                            }
                        }
                        navAgent.destination = transform.position;
                    }
                    else
                    {
                        shotWaitCounter = waitBetweenShots;
                    }
                }
                anim.SetBool("isMove", false);
            }
        }
    }










}
