using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint1;
    public Transform firePoint2;
    public float shootCooldown = 0.5f;

    private int firePointIndex = 0;
    private float lastShotTime = 0f;

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame || Gamepad.current?.rightTrigger.wasPressedThisFrame == true)
        {
            if (Time.time >= lastShotTime + shootCooldown)
            {
                Shoot();
                lastShotTime = Time.time;
            }
        }
    }

    void Shoot()
    {
        Transform selectedFirePoint = (firePointIndex == 0) ? firePoint1 : firePoint2;
        Instantiate(projectilePrefab, selectedFirePoint.position, selectedFirePoint.rotation);

        firePointIndex = 1 - firePointIndex; // Switch fire point for next shot
    }
}
