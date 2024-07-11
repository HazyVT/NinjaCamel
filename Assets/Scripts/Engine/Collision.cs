using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collision : MonoBehaviour
{

    public float invincibilityTime;
    public GameObject toCollideWith;
    public float magnitude;

    // For testing purposes only
    public Text invincText;

    private bool hasCollided = false;
    private float duration;

    void Start()
    {
        duration = invincibilityTime;
    }

    void Update()
    {

        if (hasCollided)
        {
            duration -= Time.deltaTime;
        }

        CheckInvincibility();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!hasCollided)
        {
            hasCollided = true;
        }
    }

    private void CheckInvincibility()
    {
        if (duration <= 0)
        {
            hasCollided = false;
            duration = invincibilityTime;
        }
    }
}
