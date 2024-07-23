using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{

    public float movementSpeed;
    public float accelerationSpeed;
    public float deaccelerationSpeed;
    private float horizontalVelocity;
    private float verticalVelocity;

    public int health;
    public float invincibilityTime;
    private bool hasCollided;
    private float invicDuration;

    public float bulletSpawningTimeInterval;
    private float bulletSpawnDuration;
    private float middleDuration;
    private bool firstSpawn;
    public GameObject bulletPrefab;
    public GameObject bulletHolder;

    public float meleeAttackTimeInterval;
    private float meleeAttackDuration;
    private float meleeWaitTime = 1;
    public GameObject meleePrefab;

    public Joystick joystick;
    public SpriteRenderer sr;
    private Rigidbody2D rb;



    private float facing = 1;
    private int currentLevel;

    public GameObject orbitWeapon;
    private bool spawnedOrbit = false;

    public ParticleSystem particleSystem;
    private float deathDuration = 1;
    private bool particleHasPlayed = false;

    public GameObject cameraHolder;

    private float lightningDuration;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        bulletSpawningTimeInterval = Globals.shurikenFireSpeed;
        bulletSpawnDuration = bulletSpawningTimeInterval;
        invicDuration = invincibilityTime;
        HealthManager.health = health;
        meleeAttackDuration = meleeAttackTimeInterval;
        currentLevel = Globals.shurikenLevel;
        movementSpeed = Globals.playerSpeed;
        lightningDuration = Globals.lightningFireSpeed;
        middleDuration = 0.4f;
        firstSpawn = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (!ExperienceManager.isLeveling)
        {
            movementSpeed = Globals.playerSpeed;

            if (HealthManager.health <= 0 && !particleHasPlayed)
            {
                particleSystem.Play();
                sr.sprite = null;
                particleHasPlayed = true;
            } else if (particleHasPlayed && HealthManager.health <= 0)
            {
                deathDuration -= Time.deltaTime;

                if (deathDuration <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }

            if (Globals.hasLightning)
            {
                lightningDuration -= Time.deltaTime;

                if (lightningDuration <= 0)
                {
                    GetComponent<LightningWeaponScript>().StrikeLightning();
                    lightningDuration = Globals.lightningFireSpeed;
                    cameraHolder.GetComponent<CameraScript>().CauseShake(0.3f, 0.2f);
                }
            }

            bulletSpawningTimeInterval = Globals.shurikenFireSpeed;

            float xscale = Utilities.Approach(transform.localScale.x, 1.2f, Time.deltaTime / 6);
            float yscale = Utilities.Approach(transform.localScale.y, 1.2f, Time.deltaTime / 6);

            transform.localScale = new(xscale, yscale, 1);

            if (joystick.Horizontal < 0)
            {
                sr.flipX = true;
            }
            else if (joystick.Horizontal > 0)
            {
                sr.flipX = false;
            }

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length != 0)
            {
                bulletSpawnDuration -= Time.deltaTime;

                if (bulletSpawnDuration <= 0)
                {
                    if (!firstSpawn)
                    {
                        GetComponent<ShurikenScript>().SpawnShuriken(facing);
                        transform.localScale = new(1,1,1);
                        firstSpawn = true;
                    }


                    if (Globals.shurikenLevel == 3)
                    {

                        middleDuration -= Time.deltaTime;

                        if (middleDuration <= 0)
                        {
                            GetComponent<ShurikenScript>().SpawnShuriken(facing);
                            bulletSpawnDuration = bulletSpawningTimeInterval;
                            middleDuration = 0.4f;
                            firstSpawn = false;

                        }
                    } else
                    {
                        bulletSpawnDuration = bulletSpawningTimeInterval;
                        firstSpawn = false;
                    }

                    

                }

                if (Globals.hasMelee)
                {
                    meleeAttackDuration -= Time.deltaTime;

                    if (meleeAttackDuration <= 0)
                    {

                        switch (Globals.meleeLevel)
                        {
                            case 1:
                                Instantiate(meleePrefab, new(transform.position.x + facing * 2, transform.position.y, transform.position.z), Quaternion.identity, transform);
                                meleeAttackDuration = meleeAttackTimeInterval;
                                break;
                            case 2:
                            default:
                                Instantiate(meleePrefab, new(transform.position.x + facing * 2, transform.position.y, transform.position.z), Quaternion.identity, transform);
                                print("Melee");
                                while (meleeWaitTime > 0)
                                {
                                    meleeWaitTime -= Time.deltaTime;
                                }

                                if (meleeWaitTime <= 0)
                                {
                                    Instantiate(meleePrefab, new(transform.position.x - facing * 2, transform.position.y, transform.position.z), Quaternion.identity, transform);
                                    meleeWaitTime = 1;
                                }
                           
                                meleeAttackDuration = meleeAttackTimeInterval;
                                break;

                        }
                        
                    }
                }

                switch (Globals.chakramLevel)
                {
                    case 1:
                        if (Globals.hasChakram && spawnedOrbit == false)
                        {
                            orbitWeapon.GetComponent<OrbitingWeapon>().player = gameObject.transform;
                            Instantiate(orbitWeapon, transform.position, Quaternion.identity);
                            spawnedOrbit = true;
                        }
                        break;
                    case 2:
                        break;
                }

                
            }

            if (!particleHasPlayed) 
            {
                if (joystick.Horizontal != 0)
                {
                    horizontalVelocity = Utilities.Approach(horizontalVelocity, movementSpeed * joystick.Horizontal, accelerationSpeed);
                    facing = Mathf.Sign(joystick.Horizontal);
                }
                else
                {
                    horizontalVelocity = 0;
                }

                if (joystick.Vertical != 0)
                {
                    verticalVelocity = Utilities.Approach(verticalVelocity, movementSpeed * joystick.Vertical, accelerationSpeed);
                }
                else
                {
                    verticalVelocity = 0;
                }
            } else {
                horizontalVelocity = 0;
                verticalVelocity = 0;
                ExperienceManager.isLeveling = false;
            }
            

            rb.velocity = new(horizontalVelocity, verticalVelocity);

            if (hasCollided)
            {
              invicDuration -= Time.deltaTime;
            }

            if (invicDuration <= 0)
            {
                hasCollided = false;
                invicDuration = invincibilityTime;
                sr.color = Color.white;
            }
        }
        else
        {
            rb.velocity = new(0, 0);
        }


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!hasCollided && collision.gameObject.CompareTag("Enemy"))
        {
            hasCollided = true;
            cameraHolder.GetComponent<CameraScript>().CauseShake(0.5f, 0.7f);
            sr.color = Color.red;
            HealthManager.changeHealth(-10);
        }
    }

}
