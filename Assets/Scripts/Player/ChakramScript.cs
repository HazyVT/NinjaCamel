using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakramScript : MonoBehaviour
{

    private bool spawnedChakram = false;
    private bool spawnedSecondChakram = false;
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
        else if (Globals.chakramLevel == 2 && !spawnedSecondChakram)
        {
            float firstChakramAngle = chakram.GetComponent<OrbitingWeapon>().angle;
            float secondChakramAngle = firstChakramAngle + 180;
            GameObject secondChakram = Instantiate(chakram, transform.position, Quaternion.identity);
            secondChakram.GetComponent<OrbitingWeapon>().player = gameObject.transform;
            secondChakram.GetComponent<OrbitingWeapon>().angle = secondChakramAngle;
            spawnedSecondChakram = true;

        }
    }
}
