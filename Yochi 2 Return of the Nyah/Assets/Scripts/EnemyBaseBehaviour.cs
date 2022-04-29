using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;

public class EnemyBaseBehaviour : EnemyParent
{
    public float speed;
    public float minDistanceToPlayer;
    public float maxDistanceToPlayer;
    public BulletEmitter emitter;
    [HideInInspector]
    public bool canShoot = true;
    private bool isMoving;
    public float salveCD;

    public LayerMask playerWallMask;
    private float currentSalveCD;

    void Update()
    {        
        RotateEmitter();

        if (IsObstacleInSight())
            MoveAgent();
        else
            AdjustDistance();

        animator.SetBool("isMoving", isMoving);

        if (yokaiAnimator != null)
        {
            yokaiAnimator.SetBool("isMoving", isMoving);
        }

        UpdateYokaiDisplay();
        UpdateShoot();
    }


    public void UpdateShoot()
    {
        if(canShoot)
        {
            animator.SetBool("isAttacking", true);

            if (yokaiAnimator != null)
            {
                yokaiAnimator.SetBool("isAttacking", true);
            }
        }
        else
        {
            animator.SetBool("isAttacking", false);

            if (yokaiAnimator != null)
            {
                yokaiAnimator.SetBool("isAttacking", false);
            }
        }


        if (currentSalveCD <= 0)
        {
            if(canShoot)
            {
                Shoot();
            }
        }
        else
        {
            currentSalveCD -= Time.deltaTime;
        }  
    }

    private void Shoot()
    {
        currentSalveCD = salveCD;
        emitter.Play();
    }

    public void RotateEmitter()
    {
        Vector2 look = playerTransform.position - emitter.transform.position;
        if (look.normalized.x < 0)
        {
            animator.gameObject.GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);
        }
        emitter.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, look));
    }

    private bool IsObstacleInSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (playerTransform.position - transform.position), Mathf.Infinity, playerWallMask);
        if (hit.collider.gameObject.Equals(YochiManager.instance.gameObject))
        {
            return false;
        }

        return true;
    }

    private void MoveAgent()
    {
        targetPosition = playerTransform.position;
        CalculatePath();
        UpdateDirection();
        rb.velocity = pathDirection * speed;
        canShoot = false;
    }

    public void AdjustDistance()
    {
        Vector2 distanceToPlayer = transform.position - playerTransform.position;
        if (distanceToPlayer.magnitude < minDistanceToPlayer)
        {
            targetPosition = (Vector2)playerTransform.position + (distanceToPlayer.normalized * maxDistanceToPlayer);
            CalculatePath();
            UpdateDirection();
            rb.velocity = pathDirection * speed;
            canShoot = false;
            isMoving = true;
        }

        else if (distanceToPlayer.magnitude > maxDistanceToPlayer)
        {
            targetPosition = (Vector2)playerTransform.position + distanceToPlayer.normalized * minDistanceToPlayer;
            CalculatePath();
            UpdateDirection();
            rb.velocity = pathDirection * speed;
            //emitter.Stop();
            canShoot = false;
            animator.SetBool("isAttacking", false);
            if (yokaiAnimator != null)
            {
                yokaiAnimator.SetBool("isAttacking", false);
            }
            isMoving = true;
        }

        else
        {
            if (isMoving)
            {

                isMoving = false;
                rb.velocity = Vector3.zero;
                canShoot = true;
                /*
                if (pathEndReached)
                {
                }
                else
                {
                    isMoving = true;
                    rb.velocity = pathDirection * speed;
                }
                */
            }
            
        }
    }
}
