using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour
{
    public float speed = 15f;         // Velocidad hacia adelante
    public float rotateSpeed = 250f;  // Velocidad de rotación para seguir al objetivo
    public GameObject explosionEffect;
    public ParticleSystem smokeTrail;
    public float lifeTime = 3f;       // Tiempo antes de desactivarse automáticamente

    private Transform target;
    private LayerMask enemyLayer;

    void OnEnable()
    {
        target = null;
        Physics.SyncTransforms();
        StartCoroutine(AutoDeactivate());
    }

    IEnumerator AutoDeactivate()
    {
        yield return new WaitForSeconds(lifeTime);
        DeactivateProjectile();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetEnemyLayer(LayerMask layer)
    {
        enemyLayer = layer;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Rota suavemente hacia el objetivo
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        // Avanza hacia adelante
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            other.gameObject.SetActive(false);
        }
        print(other.gameObject.name);
        DeactivateProjectile();
    }

    void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        DeactivateProjectile();
    }

    void DeactivateProjectile()
    {
        gameObject.SetActive(false);
    }
}
