using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [Header("Shoot")]
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float fireRate = 1.5f;
    private bool canShoot = true;

    [Header("Enemy detection")]
    public float detectionRadius = 10f;
    public LayerMask enemyLayer;
    public Transform cameraTransform;

    [Header("Effects")]
    public AudioSource shootSound;
    public ParticleSystem muzzleFlash;
    public Animator reloadAnimator;

    [Header("Object Pooling")]
    public int poolSize = 10;
    private List<GameObject> projectilePool;

    [Header("Cinemachine Impulse")]
    public CinemachineImpulseSource impulseSource;

    void Awake()
    {
        projectilePool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            projectile.name = "ProyectilInicial";
            projectilePool.Add(projectile);
        }
    }

    public void Fire()
    {
        if (canShoot)
        {
            Transform target = GetClosestEnemy();
            StartCoroutine(ShootCooldown());

            GameObject projectile = GetProjectileFromPool();
            if (projectile != null)
            {
                projectile.transform.position = firePoint.position;
                projectile.transform.rotation = firePoint.rotation;
                projectile.SetActive(true);

                ProjectileController projectileScript = projectile.GetComponent<ProjectileController>();
                projectileScript.SetTarget(target);
                projectileScript.SetEnemyLayer(enemyLayer);
                if (target)
                {
                    projectile.transform.LookAt(target.position);
                }
            }
            else
            {
                projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                projectilePool.Add(projectile);
                projectile.SetActive(true);

                ProjectileController projectileScript = projectile.GetComponent<ProjectileController>();
                projectileScript.SetTarget(target);
                projectileScript.SetEnemyLayer(enemyLayer);
                if (target)
                {
                    projectile.transform.LookAt(target.position);
                }
            }

            //muzzleFlash.Play();
            //shootSound.Play();

            // Cinemachine Impulse (nuevo camera shake)
            if (impulseSource != null)
            {
                print("Generando Impulso!");
                impulseSource.GenerateImpulse();
            }
            else
            {
                print("Impulse Source no asignado!");
            }
        }
    }

    GameObject GetProjectileFromPool()
    {
        foreach (GameObject projectile in projectilePool)
        {
            if (!projectile.activeInHierarchy)
            {
                return projectile;
            }
        }
        return null;
    }

    Transform GetClosestEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
        Transform bestTarget = null;
        float closestAngle = float.MaxValue;

        foreach (Collider enemy in enemies)
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(enemy.transform.position);
            float angle = Mathf.Abs(screenPoint.x - (Screen.width / 2));

            if (angle < closestAngle)
            {
                closestAngle = angle;
                bestTarget = enemy.transform;
            }
        }
        return bestTarget;
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false;
        //reloadAnimator.SetTrigger("Reload");
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
