using System.Collections;
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
    public CameraShake cameraShake;

    public void Fire()
    {
        if (canShoot)
        {
            print("CanShoot");
            Transform target = GetClosestEnemy();
            StartCoroutine(ShootCooldown());

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            ProjectileController projectileScript = projectile.GetComponent<ProjectileController>();
            projectileScript.SetTarget(target);

            //muzzleFlash.Play();
            //shootSound.Play();
            //StartCoroutine(cameraShake.Shake(0.1f, 0.2f));
        }
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
