using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject player;

    public GameObject enemyHolder;
    public GameObject[] spawnPointArray;
    private GameObject[] enemyArray;

    public GameObject[] waveOneEnemyArray;
    public GameObject[] waveTwoEnemyArray;
    public GameObject[] waveThreeEnemyArray;

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
        enemyArray = waveOneEnemyArray;

    }

    // Update is called once per frame
    void Update()
    {
        //float waveTime = WaveManager.wave / 2;
        fullTime = timeInterval;

        switch (WaveManager.wave)
        {
            case 1:
                enemyArray = waveOneEnemyArray;
                break;
            case 2:
                enemyArray = waveTwoEnemyArray;
                break;
            case 3:
                enemyArray = waveThreeEnemyArray;
                break;
        }

        if (!ExperienceManager.isLeveling)
        {
            transform.position = player.transform.position;
            
            
            duration -= Time.deltaTime;

            if (duration <= 0)
            {
                for (int i = 0; i < WaveManager.wave; i++)
                {
                    int randomEnemyChoice = Random.Range(0, enemyArray.Length);
                    GameObject toSpawn = enemyArray[randomEnemyChoice];
                    GameObject spawnPoint = FindSpawnPoint();
                    Vector3 spawnPosition = new(spawnPoint.transform.position.x + (Random.insideUnitSphere.x * 4), spawnPoint.transform.position.y + (Random.insideUnitSphere.y * 4), spawnPoint.transform.position.z);
                    Instantiate(toSpawn, spawnPosition, Quaternion.identity, enemyHolder.transform);
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
