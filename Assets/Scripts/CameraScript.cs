using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    private Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;

    public GameObject player;

    public bool bounds;

    public Vector3 minCamearPos;
    public Vector3 maxCamearPos;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new(posX, posY, transform.position.z);

        if (bounds)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, minCamearPos.x, maxCamearPos.x),
                Mathf.Clamp(transform.position.y, minCamearPos.y, maxCamearPos.y),
                Mathf.Clamp(transform.position.z, minCamearPos.z, maxCamearPos.z)
            );
        }
    }
}
