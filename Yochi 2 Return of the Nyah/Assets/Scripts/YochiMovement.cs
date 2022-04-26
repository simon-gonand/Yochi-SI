using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YochiMovement : MonoBehaviour
{
    public float movementMaxSpeed;

    private Vector2 targetMovementVelocity;
    private Vector2 leftJoytsickInput;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        leftJoytsickInput = new Vector2(Input.GetAxis("LeftStickH"), Input.GetAxis("LeftStickV"));

        targetMovementVelocity = Vector2.ClampMagnitude(leftJoytsickInput, 1);

        rb.velocity = leftJoytsickInput * movementMaxSpeed;
    }
}
