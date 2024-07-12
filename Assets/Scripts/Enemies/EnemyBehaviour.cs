using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public float speed;
    public ParticleSystem dust;

    public int health;
    public GameObject xpPrefab;

    private GameObject player;
    public SpriteRenderer sr;
    private Rigidbody2D rb;
    private GameObject holder;

    public GameObject hit10;
    public GameObject hit20;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        holder = GameObject.FindGameObjectWithTag("XP");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ExperienceManager.isLeveling)
        {
            rb.position = Vector2.MoveTowards(rb.position, player.transform.position, speed * Time.deltaTime);
            Vector2 diff = (rb.position - (Vector2)player.transform.position);
            int horizontalCheck = (int)Mathf.Sign(diff.x);
            if (horizontalCheck == -1)
            {
                sr.flipX = true;
            } else
            {
                sr.flipX = false;
            }

            float xscale = Utilities.Approach(transform.localScale.x, 1, 2 * Time.deltaTime);
            float yscale = Utilities.Approach(transform.localScale.y, 1, 2 * Time.deltaTime);

            transform.localScale = new(xscale, yscale, 1);
        }
    }
        /*
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            OnBulletHit(10, collision);
        }
        else if (collision.CompareTag("Orbit"))
        {
            OnOrbitHit(10);
        }
        else if (collision.CompareTag("Melee"))
        {
            OnMeleeHit(10);
        }
    }
        */


    public void OnMeleeHit(int reduction)
    {
        OnOrbitHit(reduction);
    }


    public void OnOrbitHit(int reduction)
    {
        health -= reduction;
        Instantiate(hit10, transform.position, Quaternion.identity);

        if (health <= 0)
        {
            
            CreateDust();
            Instantiate(xpPrefab, transform.position, Quaternion.identity, holder.transform);
            Destroy(gameObject);
            Globals.points += 10;
            sr.transform.localScale = new Vector3(0, 0, 0);

            ExperienceManager.ChangeExperience(10);
            if (ExperienceManager.experience >= ExperienceManager.requiredExperience)
            {
                ExperienceManager.GainLevel();
            }
        }
    }

    public void OnBulletHit(int reduction, GameObject bullet)
    {
        health -= reduction;
        transform.localScale = new(1.4f, 1.4f, 1f);
        Instantiate(hit10, transform.position, Quaternion.identity);

        if (health <= 0)
        {
            CreateDust();
            Instantiate(xpPrefab, transform.position, Quaternion.identity, holder.transform);
            Globals.points += 10;
            Destroy(gameObject);
            sr.transform.localScale = new Vector3(0, 0, 0);

           
        }
        else
        {
            Destroy(bullet);
        }
    }

    private void CreateDust() {
        dust.Play();
    }

}
