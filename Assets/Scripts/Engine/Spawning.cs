using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{

    public float timeInterval;
    private float duration;

    public void SetTimeInterval(float newTime)
    {
        this.timeInterval = newTime;
        this.duration = newTime;
    }

    public void SpawnGameObject(GameObject toSpawn, GameObject spawnUnder, Vector3 position)
    {
        duration -= Time.deltaTime;

        if (duration <= 0)
        {
            Instantiate(toSpawn, position, Quaternion.identity, spawnUnder.transform);
            duration = timeInterval;
        }
    }

}
