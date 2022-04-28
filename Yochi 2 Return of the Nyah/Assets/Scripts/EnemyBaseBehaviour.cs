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
    public Animator animator;
    public bool canShoot = true;
    private bool isMoving;


    void Update()
    {        
        RotateEmitter();

        if (CheckObstacle())
            MoveAgent();
        else
            AdjustDistance();
    }
    

    public void StartShooting()
    {
        if(canShoot)
        {
            emitter.Play();
            canShoot = false;
        }        
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

    private bool CheckObstacle()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (playerTransform.position - transform.position), Mathf.Infinity);
        if (hit.collider.gameObject.Equals(YochiManager.instance.gameObject))
            return false;
        return true;
    }

    private void MoveAgent()
    {
        targetPosition = playerTransform.position;
        CalculatePath();
        UpdateDirection();
        GetComponentInParent<Rigidbody2D>().velocity = pathDirection * speed;
    }

    public void AdjustDistance()
    {
        Vector2 distanceToPlayer = transform.position - playerTransform.position;

        if (Vector2.Distance(transform.position, playerTransform.position) < minDistanceToPlayer)
        {
            targetPosition = (Vector2)playerTransform.position + (distanceToPlayer.normalized * maxDistanceToPlayer);
            CalculatePath();
            UpdateDirection();
            GetComponentInParent<Rigidbody2D>().velocity = pathDirection * speed;
            StartShooting();
            isMoving = true;
        }

        else if (Vector2.Distance(transform.position, playerTransform.position) > maxDistanceToPlayer)
        {
            targetPosition = (Vector2)playerTransform.position + distanceToPlayer.normalized * minDistanceToPlayer;
            CalculatePath();
            UpdateDirection();
            GetComponentInParent<Rigidbody2D>().velocity = pathDirection * speed;
            emitter.Stop();
            canShoot = true;
            isMoving = true;
        }

        else
        {
            if (isMoving)
            {
                GetComponentInParent<Rigidbody2D>().velocity = Vector3.zero;
                isMoving = false;
                StartShooting();
            }
            
        }
    }
}
