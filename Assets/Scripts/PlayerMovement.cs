using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody rb;
    private float speed = 3.0f;
    private Vector2 input;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        input = playerInput.actions["Move"].ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);
        Vector3 newPosition = rb.position + moveDirection * speed * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);
    }
}
