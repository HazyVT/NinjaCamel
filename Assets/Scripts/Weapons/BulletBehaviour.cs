using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletBehaviour : MonoBehaviour
{
    public float speed;
    public int damage;

    private GameObject closestEnemy;
    private Vector3 movementVector;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        FindClosestEnemy(enemies, players[0].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ExperienceManager.isLeveling)
        {

            //float xscale = Utilities.Approach(transform.localScale.x, 1, 2 * Time.deltaTime);
            //float yscale = Utilities.Approach(transform.localScale.y, 1, 2 * Time.deltaTime);

            //transform.localScale = new(xscale, yscale, 1);

            
            if (closestEnemy != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, speed * Time.deltaTime);
            }
            else
            {
                float xpos = Utilities.Approach(transform.position.x, movementVector.x + transform.position.x, speed * Time.deltaTime);
                float ypos = Utilities.Approach(transform.position.y, movementVector.y + transform.position.y, speed * Time.deltaTime);
                transform.position = new(xpos, ypos, transform.position.z);

                Vector3 camerapos = Camera.main.transform.position;
                if (transform.position.x <= camerapos.x - 10 || transform.position.x >= camerapos.x + 10 || transform.position.y <= camerapos.y - 18 || transform.position.y >= camerapos.y + 18)
                {
                    Destroy(gameObject);
                }
            }

            transform.Rotate(new Vector3(0, 0, 1));
        }
    }

    private void FindClosestEnemy(GameObject[] enemies, Vector3 position)
    {
        Transform tmin = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            Transform tform = enemy.transform;
            float dist = Vector3.Distance(tform.position, position);
            if (dist < minDist)
            {
                tmin = tform;
                minDist = dist;
                closestEnemy = enemy;
                movementVector = (enemy.transform.position - transform.position).normalized;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().OnBulletHit(damage, gameObject);
            //transform.localScale = new(1.4f, 1.4f, 1);
        }
    }
}
