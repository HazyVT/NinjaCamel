using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public float shake = 0;
    private float shakeAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shake > 0)
        {
            Vector3 shakeVector = Random.insideUnitSphere * shakeAmount;
            transform.localPosition = new(shakeVector.x, shakeVector.y, -24);
            shake -= Time.deltaTime;
        } else {
            shake = 0;
        }
    }

    public void CauseShake(float _shake, float _shakeAmount)
    {
        shake = _shake;
        shakeAmount = _shakeAmount;
    }
}
