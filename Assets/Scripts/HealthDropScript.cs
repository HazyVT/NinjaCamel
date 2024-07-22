using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthDropScript : MonoBehaviour
{

    private int healthAmount = 10;
    public GameObject text;

    //private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
       // playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //if (playerTransform == null) return;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManager.changeHealth(10);
            if (HealthManager.health > 100)
            {
                HealthManager.health = 100;
            }
            Destroy(gameObject);
        }
    }

}
