using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public float speed;
    public ParticleSystem dust;
    public ParticleSystem hitParticle;

    public int health;
    public GameObject xpPrefab;

    private GameObject player;
    public SpriteRenderer sr;
    private Rigidbody2D rb;
    private GameObject holder;

    public GameObject hit10;
    public GameObject hit20;

    public GameObject hitSound;
    private float hitDuration;
    private float hitTime = 0.3f;
    private bool hasBeenHit = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        holder = GameObject.FindGameObjectWithTag("XP");
        rb = GetComponent<Rigidbody2D>();
        //hitSound = GetComponent<AudioSource>();
        //hitSound.Stop();

    }

    // Update is called once per frame
    void Update()
    {
        if (!ExperienceManager.isLeveling)
        {

            /*
            if (transform.position.x > 30 || transform.position.x < 30 || transform.position.y > 20 || transform.position.y < -20)
            {
                float _xpos = UnityEngine.Random.Range(-25, 25);
                float _ypos = UnityEngine.Random.Range(-15, 15);
                transform.position = new(_xpos, _ypos);
            }
            */

            rb.position = Vector2.MoveTowards(rb.position, player.transform.position, speed * Time.deltaTime);
            Vector2 diff = rb.position - (Vector2)player.transform.position;
            int horizontalCheck = (int)Mathf.Sign(diff.x);
            if (horizontalCheck == -1)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }

            float xscale = Utilities.Approach(transform.localScale.x, 1, 2 * Time.deltaTime);
            float yscale = Utilities.Approach(transform.localScale.y, 1, 2 * Time.deltaTime);

            transform.localScale = new(xscale, yscale, 1);

            if (hasBeenHit)
            {
                hitDuration -= Time.deltaTime;

                if (hitDuration <= 0)
                {
                    hitSound.SetActive(false);
                    hitDuration = hitTime;
                    hasBeenHit = false;
                }
            }
        }
    }

    public void OnMeleeHit(int reduction)
    {
        OnOrbitHit(reduction);
    }


    public void OnOrbitHit(int reduction)
    {
        health -= reduction;
        transform.localScale = new(1.4f, 1.4f, 1f);
        //hitParticle.Play();
        //Instantiate(hit10, transform.position, Quaternion.identity);
        SpawnInDamageText(reduction);

        if (health <= 0)
        {
            CreateDust();
            hitSound.SetActive(true);
            Instantiate(xpPrefab, transform.position, Quaternion.identity, holder.transform);
            Destroy(gameObject);
            sr.transform.localScale = new Vector3(0, 0, 0);
            //hitSound.SetActive(true);
            //hitSound.Play();
            hitDuration = hitTime;
            hasBeenHit = true;
        }
    }

    public void OnBulletHit(int reduction, GameObject bullet)
    {
        health -= reduction;
        transform.localScale = new(1.4f, 1.4f, 1f);
        //Instantiate(hit10, transform.position, Quaternion.identity);
        SpawnInDamageText(reduction);
        //hitSound.Play();
        hitDuration = hitTime;
        hasBeenHit = true;

        // Get angle between shuriken and enemy
        float angle = Vector3.Angle(bullet.transform.position, transform.position);
        hitParticle.transform.Rotate(new(0, 0, angle * 360));
        if (health <= 0)
        {
            CreateDust();
            hitSound.SetActive(true);
            hitParticle.Play();
            Instantiate(xpPrefab, transform.position, Quaternion.identity, holder.transform);
            Destroy(gameObject);
            sr.transform.localScale = new Vector3(0, 0, 0);


        }
        else
        {
            Destroy(bullet);
        }
    }

    private void CreateDust()
    {
        dust.Play();
    }

    private void SpawnInDamageText(int reduction)
    {
        while (reduction > 0)
        {
            if (reduction >= 20)
            {
                Instantiate(hit20, transform.position, Quaternion.identity);
                reduction -= 20;
            }
            else if (reduction >= 10 && reduction < 20)
            {
                Instantiate(hit10, transform.position, Quaternion.identity);
                reduction -= 10;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            transform.position = FindNewPosition();
        }
    }

    private Vector2 FindNewPosition()
    {
        int rMin = 5;
        int rMax = 6;
        float theta = Time.deltaTime * 100;
        int r = UnityEngine.Random.Range(rMin, rMax);
        int f = UnityEngine.Random.Range(0, 2);
        if (f == 0) f = -1;
        float x = player.transform.position.x + f * (r * Mathf.Cos(theta));
        float y = player.transform.position.y + f * (r * Mathf.Sin(theta));
        return new(x, y);
    }
}


