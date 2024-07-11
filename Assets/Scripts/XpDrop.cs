using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour
{
    public int xpAmount = 10; // Amount of XP this prefab gives
    public GameObject text;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            ExperienceManager.ChangeExperience(xpAmount);
            if (ExperienceManager.experience == 100 * ExperienceManager.level) {
                ExperienceManager.GainLevel();
            }
            Instantiate(text, transform.position, Quaternion.identity);
            Destroy(gameObject); // Destroy the XP object after it is collected
        }
    }
}
 