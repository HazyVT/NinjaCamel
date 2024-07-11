using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject player;

    public GameObject enemyHolder;
    public GameObject[] spawnPointArray;
    public GameObject[] enemyArray;
    //public GameObject enemyChoiceOne;
    //public GameObject enemyChoiceTwo;
    private float timeInterval = 0.5f;
    private float totalTime = 0;
    public Joystick joystick;

    private float fullTime;
    private float duration;


    // Start is called before the first frame update
    void Start()
    {
        fullTime = (timeInterval / WaveManager.wave);
        duration = fullTime;
    }

    // Update is called once per frame
    void Update()
    {
        //float waveTime = WaveManager.wave / 2;
        fullTime = timeInterval;

        if (!ExperienceManager.isLeveling)
        {
            transform.position = player.transform.position;
            
            
            duration -= Time.deltaTime;

            if (duration <= 0)
            {
                int randomEnemyChoice = Random.Range(0, enemyArray.Length);
                GameObject toSpawn = enemyArray[randomEnemyChoice];
                GameObject spawnPoint = FindSpawnPoint();

                for (int i = 0; i < WaveManager.wave; i++)
                {
                    Instantiate(toSpawn, spawnPoint.transform.position, Quaternion.identity, enemyHolder.transform);
                }

                duration = fullTime;
                totalTime += 1;
                if (totalTime % 60 == 0) 
                {
                    WaveManager.IncreaseWave();
                }
            }



        }

    }

    private GameObject FindSpawnPoint()
    {
        if (joystick.Horizontal > 0.5)
        {
            if (joystick.Vertical < -0.5)
            {
                return spawnPointArray[2];
            } else if (joystick.Vertical > 0.5)
            {
                return spawnPointArray[3];
            } else
            {
                return spawnPointArray[4];
            }
        } else if (joystick.Horizontal < -0.5)
        {
            if (joystick.Vertical < -0.5)
            {
                return spawnPointArray[0];
            } else if (joystick.Vertical > 0.5)
            {
                return spawnPointArray[1];
            } else
            {
                return spawnPointArray[5];
            }
        } else
        {
            if (joystick.Vertical < -0.5)
            {
                return spawnPointArray[7];
            } else if (joystick.Vertical > 0.5)
            {
                return spawnPointArray[6];
            } else
            {
                int rand = Random.Range(0, 8);
                return spawnPointArray[rand];
            }
        }
    }
}
