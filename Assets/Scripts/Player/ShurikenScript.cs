using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenScript : MonoBehaviour
{

    public GameObject shurikenPrefab;
    public GameObject shurikenHolder;
    //public GameObject thirdLevelShurikenPrefab;

    public void SpawnShuriken(float facing)
    {
        switch (Globals.shurikenLevel)
        {
            case 1:
                GameObject one = Instantiate(shurikenPrefab, transform.position, Quaternion.identity, shurikenHolder.transform);
                one.GetComponent<BulletBehaviour>().which = 0;
                one.GetComponent<BulletBehaviour>().facing = facing;
                break;
            case 2:
                GameObject straightShuriken1 = Instantiate(shurikenPrefab, transform.position, Quaternion.identity, shurikenHolder.transform);
                GameObject topRightShuriken1 = Instantiate(shurikenPrefab, transform.position, Quaternion.identity, shurikenHolder.transform);
                GameObject BottomLeftShuriken1 = Instantiate(shurikenPrefab, transform.position, Quaternion.identity, shurikenHolder.transform);
                straightShuriken1.GetComponent<BulletBehaviour>().which = 0;
                topRightShuriken1.GetComponent<BulletBehaviour>().which = 1;
                BottomLeftShuriken1.GetComponent<BulletBehaviour>().which = 2;
                straightShuriken1.GetComponent<BulletBehaviour>().facing = facing;
                topRightShuriken1.GetComponent<BulletBehaviour>().facing = facing;
                BottomLeftShuriken1.GetComponent<BulletBehaviour>().facing = facing;
                break;
            case 3:
            case 4:
                GameObject straightShuriken2 = Instantiate(shurikenPrefab, transform.position, Quaternion.identity, shurikenHolder.transform);
                GameObject topRightShuriken2 = Instantiate(shurikenPrefab, transform.position, Quaternion.identity, shurikenHolder.transform);
                GameObject bottomLeftShuriken2 = Instantiate(shurikenPrefab, transform.position, Quaternion.identity, shurikenHolder.transform);
                GameObject topShuriken2 = Instantiate(shurikenPrefab, transform.position, Quaternion.identity, shurikenHolder.transform);
                GameObject bottomShuriken2 = Instantiate(shurikenPrefab, transform.position, Quaternion.identity, shurikenHolder.transform);
                straightShuriken2.GetComponent<BulletBehaviour>().which = 0;
                topRightShuriken2.GetComponent<BulletBehaviour>().which = 1;
                bottomLeftShuriken2.GetComponent<BulletBehaviour>().which = 2;
                topShuriken2.GetComponent<BulletBehaviour>().which = 3;
                bottomShuriken2.GetComponent<BulletBehaviour>().which = 4;
                straightShuriken2.GetComponent<BulletBehaviour>().facing = facing;
                topRightShuriken2.GetComponent<BulletBehaviour>().facing = facing;
                bottomLeftShuriken2.GetComponent<BulletBehaviour>().facing = facing;
                bottomShuriken2.GetComponent<BulletBehaviour>().facing = facing;
                topShuriken2.GetComponent<BulletBehaviour>().facing = facing;
                break;
        }
    }
}
