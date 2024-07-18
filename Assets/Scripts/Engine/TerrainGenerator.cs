using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject cactus;

    // Start is called before the first frame update
    void Start()
    {
        float xpos = Random.Range(-23, 23);
        float ypos = Random.Range(19, 19);
        Instantiate(cactus, new(xpos, ypos, 0), Quaternion.identity);
    }
}
