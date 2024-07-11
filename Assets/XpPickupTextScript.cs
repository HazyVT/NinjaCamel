using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpPickupTextScript : MonoBehaviour
{

    private float time = 0.5f;
    private float duration;

    // Start is called before the first frame update
    void Start()
    {
        duration = time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new(transform.position.x, transform.position.y + 0.002f, transform.position.z);
        duration -= Time.deltaTime;

        if (duration <= 0) 
        {
            Destroy(gameObject);
        }
    }
}
