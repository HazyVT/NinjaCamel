using JetBrains.Annotations;
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
    public GameObject secondOrbitWeapon; // Add this line
    private bool spawnedOrbit = false;
    private bool spawnedSecondOrbit = false; // Add this line

    public ParticleSystem particleSystem;
    private float deathDuration = 1;
    private bool particleHasPlayed = false;

    public GameObject cameraHolder;

    public GameObject damagedSound;
    private bool damagedIsPlaying = false;
    private float soundDuration = 1f;
    private float timer;

    // Electric rod variables
    public GameObject electricRodPrefab;
    public float electricRodDamage;
    public float electricRodInterval;

    private float lightningDuration;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletSpawningTimeInterval = Globals.shurikenFireSpeed;
        bulletSpawnDuration = bulletSpawningTimeInterval;
        invicDuration = invincibilityTime;
        HealthManager.health = health;
        meleeAttackDuration = meleeAttackTimeInterval;
        currentLevel = Globals.shurikenLevel;
        timer = soundDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ExperienceManager.isLeveling)
        {

            if (damagedIsPlaying)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    damagedSound.SetActive(false);
                    damagedIsPlaying = false;
                    timer = soundDuration;
                }


            }

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
            
            bulletSpawningTimeInterval = Globals.shurikenFireSpeed;

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


                    if (Globals.shurikenLevel == 4)
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

                // Chakram
                if (Globals.hasChakram)
                {
                    GetComponent<ChakramScript>().SpawnChakram(orbitWeapon);
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

    private void SpawnOrbitingWeapon()
    {
        GameObject orbit = Instantiate(orbitWeapon, transform.position, Quaternion.identity);
        orbit.GetComponent<OrbitingWeapon>().player = gameObject.transform;
        orbit.GetComponent<OrbitingWeapon>().initialAngleOffset = 0; // Ensure the first orbit starts with 0 offset
        spawnedOrbit = true;
    }

    public void SpawnSecondChakram()
    {
        if (!spawnedSecondOrbit)
        {
            GameObject secondChakram = Instantiate(secondOrbitWeapon, transform.position, Quaternion.identity);
            OrbitingWeapon secondChakramScript = secondChakram.GetComponent<OrbitingWeapon>();
            secondChakramScript.player = gameObject.transform;
            secondChakramScript.initialAngleOffset = 180f; // Set the initial angle offset to 180 degrees
            Debug.Log("Second Chakram Initial Angle Offset: " + secondChakramScript.initialAngleOffset);
            spawnedSecondOrbit = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!hasCollided && collision.gameObject.CompareTag("Enemy"))
        {
            damagedSound.SetActive(true);
            damagedIsPlaying = true;
            hasCollided = true;
            cameraHolder.GetComponent<CameraScript>().CauseShake(0.5f, 0.7f);
            sr.color = Color.red;
            HealthManager.changeHealth(-10);
        }
    }
}
