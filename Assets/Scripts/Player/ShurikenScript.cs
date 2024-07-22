using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenScript : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject bulletHolder;

    public void SpawnShuriken(float facing)
    {
        switch (Globals.shurikenLevel)
        {
            case 1:
                GameObject straightShuriken1 = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder.transform);
                GameObject topRightShuriken1 = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder.transform);
                GameObject BottomLeftShuriken1 = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder.transform);
                straightShuriken1.GetComponent<BulletBehaviour>().which = 0;
                topRightShuriken1.GetComponent<BulletBehaviour>().which = 1;
                BottomLeftShuriken1.GetComponent<BulletBehaviour>().which = 2;
                straightShuriken1.GetComponent<BulletBehaviour>().facing = facing;
                topRightShuriken1.GetComponent<BulletBehaviour>().facing = facing;
                BottomLeftShuriken1.GetComponent<BulletBehaviour>().facing = facing;
                break;
            case 2:
                GameObject straightShuriken2 = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder.transform);
                GameObject topRightShuriken2 = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder.transform);
                GameObject bottomLeftShuriken2 = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder.transform);
                GameObject topShuriken2 = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder.transform);
                GameObject bottomShuriken2 = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletHolder.transform);
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
