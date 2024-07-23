using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    public GameObject carTerrain;
    public GameObject thingamibobTerrain;
    public GameObject kettleTerrain;
    public GameObject LampTerrain;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 carPosition = GeneratePosition();
        Instantiate(carTerrain, carPosition, Quaternion.identity);        
        Vector2 thingamibobPosition = GeneratePosition();
        Instantiate(thingamibobTerrain, thingamibobPosition, Quaternion.identity);
        Vector2 kettlePosition = GeneratePosition();
        Instantiate(kettleTerrain, kettlePosition, Quaternion.identity);
        Vector2 LampPosition = GeneratePosition();
        Instantiate(LampTerrain, LampPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector2 GeneratePosition()
    {
        float _xpos = Random.Range(-27, 27);
        float _ypos = Random.Range(-17, 17);
        return new (_xpos, _ypos);
    }
}
