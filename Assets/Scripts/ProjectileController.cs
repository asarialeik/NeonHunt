using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour
{
    public float speed = 15f;
    public float rotateSpeed = 0f;
    public float lifeTime = 3f;

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
        WaypointPatrol waypointPatrol = other.GetComponent<WaypointPatrol>()
            ?? other.GetComponentInParent<WaypointPatrol>()
            ?? other.GetComponentInChildren<WaypointPatrol>();

        if (waypointPatrol != null)
        {
            waypointPatrol.gameObject.SetActive(false);
            waypointPatrol.DisabledEnemy();
        }

        DeactivateProjectile();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            WaypointPatrol waypointPatrol = collision.gameObject.GetComponent<WaypointPatrol>()
                ?? collision.gameObject.GetComponentInParent<WaypointPatrol>()
                ?? collision.gameObject.GetComponentInChildren<WaypointPatrol>();

            if (waypointPatrol != null)
            {
                waypointPatrol.gameObject.SetActive(false);
                waypointPatrol.DisabledEnemy();
            }

            DeactivateProjectile();
        }
    }


    void DeactivateProjectile()
    {
        gameObject.SetActive(false);
    }
}
