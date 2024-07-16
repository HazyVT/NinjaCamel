using System.Collections;
using System.Collections.Generic;
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

    private float shake = 0;
    private float shakeAmount = 0.7f;

    public GameObject orbitWeapon;
    private bool spawnedOrbit = false;

    public ParticleSystem particleSystem;
    private float deathDuration = 1;
    private bool particleHasPlayed = false;

    public GameObject cameraHolder;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletSpawningTimeInterval = Globals.shurikenFireSpeed;
        bulletSpawnDuration = bulletSpawningTimeInterval;
        invicDuration = invincibilityTime;
        HealthManager.health = health;
        meleeAttackDuration = meleeAttackTimeInterval;

    }

    // Update is called once per frame
    void Update()
    {
        if (!ExperienceManager.isLeveling)
        {
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


                
            if (shake > 0)
            {
                Vector3 shakeVector = Random.insideUnitSphere * shakeAmount;
                cameraHolder.transform.localPosition = new(shakeVector.x, shakeVector.y, -24);
                shake -= Time.deltaTime;
            } else {
                shake = 0;
                //Camera.main.transform.position = new(0,0,-1);
            }

            bulletSpawningTimeInterval = Globals.shurikenFireSpeed;

            float xscale = Utilities.Approach(transform.localScale.x, 1, 2 * Time.deltaTime);
            float yscale = Utilities.Approach(transform.localScale.y, 1, 2 * Time.deltaTime);

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
                    if (Globals.shurikenLevel == 1)
                    {
                        Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder.transform);
                        bulletSpawnDuration = bulletSpawningTimeInterval;
                        transform.localScale = new(1.15f, 1.15f, 1f);
                    } else
                    {
                        GameObject straightShuriken = bulletPrefab;
                        straightShuriken.GetComponent<BulletBehaviour>().which = 0;
                        Instantiate(straightShuriken, transform.position, Quaternion.identity, bulletHolder.transform);
                        bulletSpawnDuration = bulletSpawningTimeInterval;
                        transform.localScale = new(1.15f, 1.15f, 1f);
                    }
                    
                }

                if (Globals.hasMelee)
                {
                    meleeAttackDuration -= Time.deltaTime;

                    if (meleeAttackDuration <= 0)
                    {
                        int facing = sr.flipX ? -1 : 1;
                        Instantiate(meleePrefab, new(transform.position.x + facing * 2, transform.position.y, transform.position.z), Quaternion.identity);
                        meleeAttackDuration = meleeAttackTimeInterval;
                    }
                }

                if (Globals.hasChakram && spawnedOrbit == false)
                {
                    print("Spawn");
                    orbitWeapon.GetComponent<OrbitingWeapon>().player = gameObject.transform;
                    Instantiate(orbitWeapon, transform.position, Quaternion.identity);
                    spawnedOrbit = true;
                }
            }

            if (!particleHasPlayed) 
            {
                if (joystick.Horizontal != 0)
                {
                    horizontalVelocity = Utilities.Approach(horizontalVelocity, movementSpeed * joystick.Horizontal, accelerationSpeed);
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
        if (!hasCollided)
        {
            hasCollided = true;
            shake = 0.2f;
            sr.color = Color.red;
            HealthManager.changeHealth(-10);

            if (HealthManager.health <= 0)
            {
                
            }

        }
    }

}
