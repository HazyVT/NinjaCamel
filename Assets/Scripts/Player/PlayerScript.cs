using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // Movement variables
    public float movementSpeed;
    public float accelerationSpeed;
    public float deaccelerationSpeed;
    private float horizontalVelocity;
    private float verticalVelocity;

    // Health and invincibility variables
    public int health;
    public float invincibilityTime;
    private bool hasCollided;
    private float invicDuration;

    // Bullet spawning variables
    public float bulletSpawningTimeInterval;
    private float bulletSpawnDuration;
    private float middleDuration;
    private bool firstSpawn;
    public GameObject bulletPrefab;
    public GameObject bulletHolder;

    // Melee attack variables
    public float meleeAttackTimeInterval;
    private float meleeAttackDuration;
    private float meleeWaitTime = 1;
    public GameObject meleePrefab;

    // Joystick and rendering variables
    public Joystick joystick;
    public SpriteRenderer sr;
    private Rigidbody2D rb;

    // Facing direction and current level
    private float facing = 1;
    private int currentLevel;

    // Orbit weapon variables
    public GameObject orbitWeapon;
    public GameObject secondOrbitWeapon;
    private bool spawnedOrbit = false;
    private bool spawnedSecondOrbit = false;

    // Particle system variables
    public ParticleSystem particleSystem;
    private float deathDuration = 1;
    private bool particleHasPlayed = false;

    // Camera holder
    public GameObject cameraHolder;

    // Sound effect variables
    public GameObject damagedSound;
    private bool damagedIsPlaying = false;
    private float soundDuration = 1f;
    private float timer;

    public GameObject xpSound;
    public bool xpIsPlaying = false;
    private float xpDuration = 0.2f;
    private float xpTimer;

    public GameObject thunderSound;
    public bool thunderIsPlaying = false;
    private float thunderDuration = 1.5f;
    private float thunderTimer;

    // Lightning duration
    private float lightningDuration;

    // Reference to the GameTimer script
    private GameTimer gameTimer;

    void Start()
    {
        // Initialize Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        bulletSpawningTimeInterval = Globals.shurikenFireSpeed;
        bulletSpawnDuration = bulletSpawningTimeInterval;
        invicDuration = invincibilityTime;
        HealthManager.health = health;
        meleeAttackDuration = meleeAttackTimeInterval;
        currentLevel = Globals.shurikenLevel;
        timer = soundDuration;
        xpTimer = xpDuration;
        thunderTimer = thunderDuration;

        // Initialize the GameTimer reference
        gameTimer = FindObjectOfType<GameTimer>();
    }

    void Update()
    {
        if (!ExperienceManager.isLeveling)
        {
            // Handle damaged sound effect
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

            // Handle XP sound effect
            if (xpIsPlaying)
            {
                xpTimer -= Time.deltaTime;

                if (xpTimer <= 0)
                {
                    xpSound.SetActive(false);
                    xpIsPlaying = false;
                    xpTimer = xpDuration;
                }
            }

            // Handle thunder sound effect
            if (thunderIsPlaying)
            {
                thunderTimer -= Time.deltaTime;

                if (thunderTimer <= 0)
                {
                    thunderSound.SetActive(false);
                    thunderIsPlaying = false;
                    thunderTimer = thunderDuration;
                }
            }

            movementSpeed = Globals.playerSpeed;

            // Handle player death
            if (HealthManager.health <= 0 && !particleHasPlayed)
            {
                particleSystem.Play();
                sr.sprite = null;
                particleHasPlayed = true;
            }
            else if (particleHasPlayed && HealthManager.health <= 0)
            {
                deathDuration -= Time.deltaTime;

                if (deathDuration <= 0)
                {
                 
                    SceneManager.LoadScene("GameOver"); // Load the game over scene
                }
            }

            // Handle lightning weapon
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

            // Handle player scaling
            float xscale = Utilities.Approach(transform.localScale.x, 1.2f, Time.deltaTime / 6);
            float yscale = Utilities.Approach(transform.localScale.y, 1.2f, Time.deltaTime / 6);

            transform.localScale = new(xscale, yscale, 1);

            // Handle player direction
            if (joystick.Horizontal < 0)
            {
                sr.flipX = true;
            }
            else if (joystick.Horizontal > 0)
            {
                sr.flipX = false;
            }

            bulletSpawningTimeInterval = Globals.shurikenFireSpeed;

            // Handle bullet spawning
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length != 0)
            {
                bulletSpawnDuration -= Time.deltaTime;

                if (bulletSpawnDuration <= 0)
                {
                    if (!firstSpawn)
                    {
                        GetComponent<ShurikenScript>().SpawnShuriken(facing);
                        transform.localScale = new(1, 1, 1);
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
                    }
                    else
                    {
                        bulletSpawnDuration = bulletSpawningTimeInterval;
                        firstSpawn = false;
                    }
                }

                if (Globals.hasChakram)
                {
                    GetComponent<ChakramScript>().SpawnChakram(orbitWeapon);
                }
            }

            // Handle player movement
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
            }
            else
            {
                horizontalVelocity = 0;
                verticalVelocity = 0;
                ExperienceManager.isLeveling = false;
            }

            rb.velocity = new(horizontalVelocity, verticalVelocity);

            // Handle invincibility duration
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
            damagedSound.SetActive(true);
            damagedIsPlaying = true;
            hasCollided = true;
            cameraHolder.GetComponent<CameraScript>().CauseShake(0.5f, 0.7f);
            sr.color = Color.red;
            HealthManager.changeHealth(-10);
        }
    }
}
