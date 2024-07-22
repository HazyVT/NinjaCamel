using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningWeaponScript : MonoBehaviour
{

    private float duration;

    public int times = 1;

    // Start is called before the first frame update
    void Start()
    {
        duration = Globals.lightningFireSpeed;
    }

    public void StrikeLightning()
    {
        for (int i = 0; i < times; i++)
            {
                float waitTime = 0.4f;

                while (waitTime > 0)
                {
                    waitTime -= Time.deltaTime;
                }
                print("Fire");
                // Find closest enemy
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                GameObject closest = FindClosestEnemy(enemies, transform.position);
                closest.GetComponent<EnemyBehaviour>().OnOrbitHit(Globals.lightningDamage);
            }
    }

    private GameObject FindClosestEnemy(GameObject[] enemies, Vector3 position)
    {
        GameObject closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            Transform tform = enemy.transform;
            float dist = Vector3.Distance(tform.position, position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = enemy;
            }

        }

        return closest;
    }
}
