using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefabHolder : MonoBehaviour
{
    public GameObject body;

    private float duration = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (body == null)
        {
            duration -= Time.deltaTime;

            if (duration <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
