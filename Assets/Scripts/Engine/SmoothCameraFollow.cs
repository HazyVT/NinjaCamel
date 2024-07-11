using UnityEngine;

public class SmoothCameraFollow2D : MonoBehaviour
{
    public Transform target;  
    public float smoothSpeed = 0.125f;  
    public Vector3 offset;  

    void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not set for SmoothCameraFollow2D.");
            return;
        }

        
        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}