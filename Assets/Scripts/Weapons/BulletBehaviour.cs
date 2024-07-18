using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletBehaviour : MonoBehaviour
{
    public float speed;
    public int damage;

    public int which;
    public float facing;

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
            if (Globals.shurikenLevel == 1 || Globals.shurikenLevel == 2)
            {
                if (closestEnemy != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, speed * Time.deltaTime);
                }
                else
                {
                    float xpos = Utilities.Approach(transform.position.x, movementVector.x + transform.position.x, speed * Time.deltaTime);
                    float ypos = Utilities.Approach(transform.position.y, movementVector.y + transform.position.y, speed * Time.deltaTime);
                    transform.position = new(xpos, ypos, transform.position.z);
                }
            } else
            {
                print("New");
                switch (which)
                {
                    case 0:
                        float _xpos = Utilities.Approach(transform.position.x, transform.position.x + (30 * facing), speed * Time.deltaTime);
                        transform.position = new(_xpos, transform.position.y, transform.position.z);
                        break;
                    case 1:
                        float _xpos2 = Utilities.Approach(transform.position.x, transform.position.x + (30 * facing), speed * Time.deltaTime);
                        float _ypos2 = Utilities.Approach(transform.position.y, transform.position.y + 2 * Time.deltaTime, speed * Time.deltaTime);
                        transform.position = new(_xpos2, _ypos2, transform.position.z);
                        break;
                    case 2:
                        float _xpos3 = Utilities.Approach(transform.position.x, transform.position.x + (30 * facing), speed * Time.deltaTime);
                        float _ypos3 = Utilities.Approach(transform.position.y, transform.position.y - 2 * Time.deltaTime, speed * Time.deltaTime);
                        transform.position = new(_xpos3, _ypos3, transform.position.z);
                        break;
                }
            }
            
            Vector3 camerapos = Camera.main.transform.position;
            if (transform.position.x <= camerapos.x - 10 || transform.position.x >= camerapos.x + 10 || transform.position.y <= camerapos.y - 18 || transform.position.y >= camerapos.y + 18)
            {
                Destroy(gameObject);
            }

            transform.Rotate(new Vector3(0, 0, 40));
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
                movementVector = (enemy.transform.position - position).normalized;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().OnBulletHit(damage, gameObject);
            //transform.localScale = new(1.4f, 1.4f, 1);
            //Destroy(gameObject);
        }
    }
}
