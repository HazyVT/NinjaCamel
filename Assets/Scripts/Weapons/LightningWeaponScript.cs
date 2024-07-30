using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningWeaponScript : MonoBehaviour
{
    private float duration;
    public GameObject lightningPrefab;
    private bool firstSpawn;
    private float middleDuration;
    public int times = 1;

    private PlayerScript playerScript;

    void Start()
    {
        duration = Globals.lightningFireSpeed;
        middleDuration = 0.4f;

        // Find the PlayerScript component
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    public void StrikeLightning()
    {
        if (!firstSpawn)
        {
            SpawnLightning();
            firstSpawn = true;
        }

        if (Globals.lightningLevel == 3)
        {
            middleDuration -= Time.deltaTime;
            if (middleDuration <= 0)
            {
                SpawnLightning();
                middleDuration = 0.4f;
                duration = Globals.lightningFireSpeed;
                firstSpawn = false;
            }
        }
        else
        {
            duration = Globals.lightningFireSpeed;
            firstSpawn = false;
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

    private void SpawnLightning()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return;

        GameObject closest = FindClosestEnemy(enemies, transform.position);
        Instantiate(lightningPrefab, closest.transform.position, Quaternion.identity);
        closest.GetComponent<EnemyBehaviour>().OnOrbitHit(Globals.lightningDamage);

        // Play the thunder sound
        playerScript.thunderSound.SetActive(true);
        playerScript.thunderIsPlaying = true;
    }
}
