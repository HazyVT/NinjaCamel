using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParicle : MonoBehaviour
{

    public GameObject body;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (body != null)
        {
            transform.position = body.transform.position;
        }
    }
}
