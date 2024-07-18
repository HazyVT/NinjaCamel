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
    public GameObject bulletPrefab;
    public GameObject bulletHolder;

    public float meleeAttackTimeInterval;
    private float meleeAttackDuration;
    public GameObject meleePrefab;

    public Joystick joystick;
    public SpriteRenderer sr;
    private Rigidbody2D rb;

    public float shake = 0;
    private float shakeAmount = 0.7f;

    private float facing = 1;
    private int currentLevel;

    public GameObject orbitWeapon;
    private bool spawnedOrbit = false;

    public ParticleSystem particleSystem;
    private float deathDuration = 1;
    private bool particleHasPlayed = false;

    public GameObject cameraHolder;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletSpawningTimeInterval = Globals.shurikenFireSpeed;
        bulletSpawnDuration = bulletSpawningTimeInterval;
        invicDuration = invincibilityTime;
        HealthManager.health = health;
        meleeAttackDuration = meleeAttackTimeInterval;
        currentLevel = Globals.shurikenLevel;
    }

    void Update()
    {
        if (!ExperienceManager.isLeveling)
        {
            HandleDeath();
            HandleCameraShake();
            bulletSpawningTimeInterval = Globals.shurikenFireSpeed;
            UpdatePlayerScale();
            FlipSpriteBasedOnJoystick();

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length != 0)
            {
                HandleBulletSpawning();

                if (Globals.hasMelee)
                {
                    HandleMeleeAttack();
                }

                if (Globals.hasChakram && !spawnedOrbit)
                {
                    SpawnOrbitingWeapon();
                }
            }

            if (!particleHasPlayed)
            {
                HandleMovement();
            }
            else
            {
                StopMovement();
            }

            HandleInvincibility();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void HandleDeath()
    {
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void HandleCameraShake()
    {
        if (shake > 0)
        {
            Vector3 shakeVector = UnityEngine.Random.insideUnitSphere * shakeAmount;
            cameraHolder.transform.localPosition = new Vector3(shakeVector.x, shakeVector.y, -24);
            shake -= Time.deltaTime;
        }
        else
        {
            shake = 0;
        }
    }

    private void UpdatePlayerScale()
    {
        float xscale = Utilities.Approach(transform.localScale.x, 1, 2 * Time.deltaTime);
        float yscale = Utilities.Approach(transform.localScale.y, 1, 2 * Time.deltaTime);
        transform.localScale = new Vector3(xscale, yscale, 1);
    }

    private void FlipSpriteBasedOnJoystick()
    {
        if (joystick.Horizontal < 0)
        {
            sr.flipX = true;
        }
        else if (joystick.Horizontal > 0)
        {
            sr.flipX = false;
        }
    }

    private void HandleBulletSpawning()
    {
        bulletSpawnDuration -= Time.deltaTime;

        if (bulletSpawnDuration <= 0)
        {
            currentLevel = Globals.shurikenLevel;
            if (currentLevel == 1 || currentLevel == 2)
            {
                GameObject shuriken = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder.transform);
                bulletSpawnDuration = bulletSpawningTimeInterval;
                transform.localScale = new Vector3(1.15f, 1.15f, 1f);
            }
            else
            {
                SpawnMultipleShurikens();
            }
        }
    }

    private void SpawnMultipleShurikens()
    {
        GameObject straightShuriken = Instantiate(bulletPrefab, transform.position, quaternion.identity, bulletHolder.transform);
        GameObject topRightShuriken = Instantiate(bulletPrefab, transform.position, quaternion.identity, bulletHolder.transform);
        GameObject BottomLeftShuriken = Instantiate(bulletPrefab, transform.position, quaternion.identity, bulletHolder.transform);
        straightShuriken.GetComponent<BulletBehaviour>().which = 0;
        topRightShuriken.GetComponent<BulletBehaviour>().which = 1;
        BottomLeftShuriken.GetComponent<BulletBehaviour>().which = 2;
        straightShuriken.GetComponent<BulletBehaviour>().facing = facing;
        topRightShuriken.GetComponent<BulletBehaviour>().facing = facing;
        BottomLeftShuriken.GetComponent<BulletBehaviour>().facing = facing;
        bulletSpawnDuration = bulletSpawningTimeInterval;
        transform.localScale = new Vector3(1.15f, 1.15f, 1f);
    }

    private void HandleMeleeAttack()
    {
        meleeAttackDuration -= Time.deltaTime;

        if (meleeAttackDuration <= 0)
        {
            StartCoroutine(PerformMeleeAttack());
            meleeAttackDuration = meleeAttackTimeInterval;
        }
    }

    private IEnumerator PerformMeleeAttack()
    {
        int facing = sr.flipX ? -1 : 1;

        if (Globals.meleeLevel == 1)
        {
            // First upgrade: Only attack in front
            Instantiate(meleePrefab, new Vector3(transform.position.x + facing * 2, transform.position.y, transform.position.z), Quaternion.identity);
        }
        else if (Globals.meleeLevel == 2)
        {
            // Second upgrade: Attack forward then backward
            Instantiate(meleePrefab, new Vector3(transform.position.x + facing * 2, transform.position.y, transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            Instantiate(meleePrefab, new Vector3(transform.position.x - facing * 2, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 180));
        }
    }

    private void SpawnOrbitingWeapon()
    {
        print("Spawn");
        orbitWeapon.GetComponent<OrbitingWeapon>().player = gameObject.transform;
        Instantiate(orbitWeapon, transform.position, Quaternion.identity);
        spawnedOrbit = true;
    }

    private void HandleMovement()
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

        rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
    }

    private void StopMovement()
    {
        horizontalVelocity = 0;
        verticalVelocity = 0;
        ExperienceManager.isLeveling = false;
    }

    private void HandleInvincibility()
    {
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!hasCollided)
        {
            hasCollided = true;
            shake = 0.2f;
            sr.color = Color.red;
            HealthManager.changeHealth(-10);

            if (HealthManager.health <= 0)
            {
                // Handle death
            }
        }
    }
}
