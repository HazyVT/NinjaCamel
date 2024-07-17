using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour
{
    public int xpAmount = 10; // Amount of XP this prefab gives
    public GameObject text; // The text that appears when XP is collected
    public float attractRadius = 5f; // Radius within which the XP will be attracted to the player
    public float attractSpeed = 5f; // Speed at which the XP moves towards the player

    private Transform playerTransform;

    void Start()
    {
        // Find the player object by tag and get its transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        AttractToPlayer();
    }

    void AttractToPlayer()
    {
        if (playerTransform == null) return;

        // Calculate the distance between the XP object and the player
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= attractRadius)
        {
            // Move the XP towards the player
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, attractSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Increase the player's experience
            ExperienceManager.ChangeExperience(xpAmount);

            // Check if the player should level up
            if (ExperienceManager.experience == 100 * ExperienceManager.level)
            {
                ExperienceManager.GainLevel();
            }

            // Instantiate the text at the XP's position
            Instantiate(text, transform.position, Quaternion.identity);

            // Destroy the XP object after it is collected
            Destroy(gameObject);
        }
    }
}
