using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YochiMovement : MonoBehaviour
{
    public Animator animator;
    public float movementMaxSpeed;

    private Vector2 targetMovementVelocity;
    private Vector2 leftJoytsickInput;
    private Rigidbody2D rb;

    private bool canPlayFoosteps = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        leftJoytsickInput = new Vector2(Input.GetAxis("LeftStickH"), Input.GetAxis("LeftStickV"));

        targetMovementVelocity = Vector2.ClampMagnitude(leftJoytsickInput, 1);

        if(YochiManager.instance.currentHealthPoint > 0)
        {
            rb.velocity = leftJoytsickInput * movementMaxSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        animator.SetBool("isMoving", rb.velocity.magnitude > 0.1f);

        if (animator.GetBool("isMoving") == true && canPlayFoosteps)
        {
            StartCoroutine(PlayFootSFX());
        }
    }

    public IEnumerator PlayFootSFX()
    {
        canPlayFoosteps = false;
        AudioManager.instance.PlayFoostep();
        yield return new WaitForSeconds(0.3f);
        canPlayFoosteps = true;
    }
}
