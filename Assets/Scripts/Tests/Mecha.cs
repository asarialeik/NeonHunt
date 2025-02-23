using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mecha : MonoBehaviour
{
    public GameObject sphere;
    private Rigidbody rb;
    public float tiltAmount = 45f;

    private void Start()
    {
        rb = sphere.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        this.transform.position = sphere.transform.position;
        Vector3 velocity = rb.velocity;
        if (velocity.magnitude > 0.1f)
        {
            float tiltAngle = Mathf.Clamp(velocity.x * tiltAmount, -tiltAmount, tiltAmount);
            this.transform.rotation = Quaternion.Euler(0f, transform.localEulerAngles.y, -tiltAngle);
        }
    }
}
