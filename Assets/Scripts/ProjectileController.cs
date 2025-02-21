using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 15f;         // Velocidad hacia adelante
    public float rotateSpeed = 250f;  // Velocidad de rotación para seguir al objetivo
    public GameObject explosionEffect;
    public ParticleSystem smokeTrail;
    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null)
        {
            DestroyProjectile();
            return;
        }

        Vector3 direction = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Rota suavemente hacia el objetivo
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        // Avanza hacia adelante
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Instantiate(explosionEffect, transform.position, Quaternion.identity);
        DestroyProjectile();
        print(other.name);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
