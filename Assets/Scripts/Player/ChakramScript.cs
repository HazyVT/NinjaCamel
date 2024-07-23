using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakramScript : MonoBehaviour
{

    private bool spawnedChakram = false;
    private int chakramCount = 0;

    public void SpawnChakram(GameObject chakram)
    {
        if (Globals.chakramLevel == 1 && !spawnedChakram)
        {
            chakram.GetComponent<OrbitingWeapon>().player = gameObject.transform;
            Instantiate(chakram, transform.position, Quaternion.identity);
            spawnedChakram = true;
            chakramCount = 1;
        } 
        else if (Globals.chakramLevel == 2)
        {
            
        }
    }
}
