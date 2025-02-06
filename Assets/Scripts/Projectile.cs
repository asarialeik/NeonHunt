using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 3f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Destroy enemy
            Destroy(gameObject); // Destroy bullet
        }
    }
}
