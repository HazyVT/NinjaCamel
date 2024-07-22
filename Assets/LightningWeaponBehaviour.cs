using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningWeaponBehaviour : MonoBehaviour
{

    private float duration;
    
    // Start is called before the first frame update
    void Start()
    {
        duration = 0.3f;
        float rot = Random.Range(-20, 20);
        transform.Rotate(new(0,0,rot));
    }

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;

        if (duration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
