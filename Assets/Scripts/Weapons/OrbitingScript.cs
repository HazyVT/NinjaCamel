using UnityEngine;

public class OrbitingWeapon : MonoBehaviour
{
    public Transform player;
    public float orbitDistance = 2.0f;
    public float orbitSpeed;
    public int damage;

    private Rigidbody2D rb;
    private float angle;
    private float rot;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            Debug.LogError("Player transform not assigned.");
        }
        orbitSpeed = Globals.orbitSpeed;
    }

    void FixedUpdate()
    {
        orbitSpeed = Globals.orbitSpeed;
        if (player != null && !ExperienceManager.isLeveling)
        {
            OrbitAroundPlayer();
            rot += Time.deltaTime / 40;

            transform.Rotate(new(0,0,100));

            //transform.Rotate(new(0,0,rot));
            
        }
    }

    void OrbitAroundPlayer()
    {
        angle += orbitSpeed * Time.deltaTime;
        if (angle >= 360f)
        {
            angle -= 360f;
        }

        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * orbitDistance;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * orbitDistance;

        Vector2 offset = new Vector2(x, y);
        Vector2 newPosition = player.position + (Vector3)offset;

        // Use Rigidbody2D to set the position
        rb.MovePosition(newPosition);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))  // Ensure the enemy GameObject has the tag "Enemy"
        {
            // Call the ReduceHealth method from EnemyBehaviour
            other.GetComponent<EnemyBehaviour>().OnOrbitHit(damage);
        }
    }
}
