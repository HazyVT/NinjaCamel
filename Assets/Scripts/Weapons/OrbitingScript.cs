using UnityEngine;

public class OrbitingWeapon : MonoBehaviour
{
    public Transform player;
    public float orbitDistance = 2.0f;
    public float orbitSpeed;
    public int damage;
    public float initialAngleOffset = 0.0f; // Added this line

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
        damage = Globals.chakramDamage;
        angle = initialAngleOffset; // Set the initial angle offset
        Debug.Log("Initial Angle Offset: " + initialAngleOffset);
    }

    void FixedUpdate()
    {
        orbitSpeed = Globals.orbitSpeed;
        if (player != null && !ExperienceManager.isLeveling)
        {
            OrbitAroundPlayer();
            rot += Time.deltaTime / 40;
            transform.Rotate(new(0, 0, 100));
        }
    }

    void OrbitAroundPlayer()
    {
        angle += orbitSpeed * Time.deltaTime;
        if (angle >= 360f || angle <= -360f)
        {
            angle = 0;
        }

        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * orbitDistance;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * orbitDistance;

        Vector2 offset = new Vector2(x, y);
        Vector2 newPosition = player.position + (Vector3)offset;

        rb.MovePosition(newPosition);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBehaviour>().OnOrbitHit(damage);
        }
    }
}
