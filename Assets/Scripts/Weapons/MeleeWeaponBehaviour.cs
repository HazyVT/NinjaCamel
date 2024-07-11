using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{

    public int damage;
    private float lifetime = 0.2f;
    private float duration;

    // Start is called before the first frame update
    void Start()
    {
        duration = lifetime;
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            collider.gameObject.GetComponent<EnemyBehaviour>().OnMeleeHit(damage);
        }
    }
}
