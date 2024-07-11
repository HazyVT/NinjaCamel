using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBehaviour : MonoBehaviour
{

    //private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new((0.01f * HealthManager.health), transform.localScale.y, transform.localScale.z);  

        //time += Time.deltaTime;
        //transform.localPosition = new(transform.localPosition.x, transform.localPosition.y + (float)Math.Sin(time * 2f) * 0.05f, 0);  
    }
}
