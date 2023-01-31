using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public static PlayerControler instance;

    public CharacterController controler;
    public CameraControler mainCamera;
    public Animator anim;

    // movement and jump
    private Vector3 moveInput;
    public float moveSpeed, runSpeed, jumpForce, gravityModifier;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool canJump, canDoubleJump;


    //Camera Look
    public Transform cam;
    public float mouseSensitivity = 3f;

    //fire veriable
    public Gun acticeGun;
    public List<Gun> allGuns = new List<Gun>();
    public List<Gun> unlockableGuns = new List<Gun>();
    public int currentGun;


    private bool isScope;
    private bool isShooting;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
        
    }


    void Start()
    {
        acticeGun = allGuns[currentGun];
        acticeGun.gameObject.SetActive(true);
        
        UIManager.instance.ammoText.text = "AMMO : " + acticeGun.currentAmmo;
        UIManager.instance.currentGunImage.sprite = allGuns[currentGun].gunImage;
    }



    void Update()
    {
        PlayerMove();

        if (isShooting) { PlayerShooting(); }
    }





    private void PlayerMove()
    {
        if (!UIManager.instance.pausedPanal.activeInHierarchy && !GameManager.instance.levelEnding)
        {
            // player movement
            float yStore = moveInput.y;

            Vector3 moveVertical = transform.forward * SimpleInput.GetAxisRaw("Vertical");
            Vector3 moveHorizontal = transform.right * SimpleInput.GetAxisRaw("Horizontal");

            moveInput = moveHorizontal + moveVertical;
            moveInput.Normalize();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveInput = moveInput * runSpeed;
            }
            else { moveInput = moveInput * moveSpeed; }


            // Gravity
            moveInput.y = yStore;
            moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

            if (controler.isGrounded)
            {
                moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
            }

            // Jump
            canJump = Physics.OverlapSphere(groundCheck.position, 0.25f, groundLayer).Length > 0;



            controler.Move(moveInput * Time.deltaTime);

            anim.SetFloat("move", moveInput.magnitude);
            anim.SetBool("ground", canJump);




        }

        
        
    }



    private void PlayerJump()
    {
        // Jump Handler
        if (canJump)
        {
            moveInput.y = jumpForce;
            canDoubleJump = true;
            AudioManager.instance.PlaySFX(8);
        }
        else if (canDoubleJump)
        {
            moveInput.y = jumpForce;
            canDoubleJump = false;
            AudioManager.instance.PlaySFX(8);
        }

        controler.Move(moveInput * Time.deltaTime);
    }



    public void ShootButtonDown() { isShooting = true; }
    public void ShootButtonUp() { isShooting = false; }

    public void PlayerShooting()
    {
        if (UIManager.instance.pausedPanal.activeInHierarchy == false)
        {

            // Player Multipile Shooting
            if (acticeGun.currentAmmo > 0)
            {
                if (acticeGun.fireCounter <= 0)
                {
                    AudioManager.instance.PlaySFX(10);
                    RaycastHit hit;
                    if (Physics.Raycast(cam.position, cam.forward, out hit, 50f))
                    {
                        acticeGun.firePoint.LookAt(hit.point);
                    }
                    else { acticeGun.firePoint.LookAt(cam.position + (cam.forward * 30f)); }

                    if (acticeGun.currentAmmo > 0)
                    {
                        Instantiate(acticeGun.projectile, acticeGun.firePoint.position, acticeGun.firePoint.rotation);

                        acticeGun.currentAmmo--;
                        anim.SetTrigger("shoot");
                        UIManager.instance.ammoText.text = "AMMO : " + acticeGun.currentAmmo;

                        acticeGun.fireCounter = acticeGun.fireRate;
                    }
                }
            }

        }
    }






    // gun scope and unscope
    public void GunTargetScope()
    {
        isScope = !isScope;
        anim.SetBool("scope", isScope);
        if (isScope)
        {
            mainCamera.ZoomIn(acticeGun.zoomAmount);
            if (acticeGun.isSniper) 
            {
                acticeGun.scopeImage.SetActive(true);
                acticeGun.gunModle.SetActive(false);
            }
        }
        else
        {
            mainCamera.ZoomOut();
            if (acticeGun.isSniper) 
            {
                acticeGun.scopeImage.SetActive(false);
                acticeGun.gunModle.SetActive(true);
            }
            
        }
    }



    // Gun Switch Function
    public void GunSwitch()
    {
        acticeGun.gameObject.SetActive(false);
        acticeGun.scopeImage.SetActive(false);
        acticeGun.gunModle.SetActive(true);

        currentGun++;

        if (currentGun >= allGuns.Count)
        {
            currentGun = 0;
        }

        acticeGun = allGuns[currentGun];
        acticeGun.gameObject.SetActive(true);

        UIManager.instance.ammoText.text = "AMMO : " + acticeGun.currentAmmo;
        UIManager.instance.currentGunImage.sprite = allGuns[currentGun].gunImage;
    }



    // Add Gun Function
    public void AddGuns(string gunToAdd)
    {
        bool gunUnlocked = false;

        if (unlockableGuns.Count > 0)
        {
            for (int i = 0; i < unlockableGuns.Count; i++)
            {
                if (unlockableGuns[i].gunName == gunToAdd)
                {
                    gunUnlocked = true;

                    allGuns.Add(unlockableGuns[i]);

                    unlockableGuns.RemoveAt(i);

                    i = unlockableGuns.Count;
                }
            }
        }


        if (gunUnlocked)
        {
            currentGun = allGuns.Count - 2;
            GunSwitch();
        }
    }







}
