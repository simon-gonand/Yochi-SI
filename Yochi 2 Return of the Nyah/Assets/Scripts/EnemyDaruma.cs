using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;

public class EnemyDaruma : EnemyParent
{
    public float speed;
    public float minDistanceToPlayer;
    public float maxDistanceToPlayer;
    public BulletEmitter emitter;
    public float attackCooldown;
    [HideInInspector]
    public bool isAttacking;

    void Update()
    {
        RotateEmitter();

        MoveAgent();
    }

    public IEnumerator StartShooting()
    {
        isAttacking = true;
        attackCDLeft = attackCooldown;
        animator.SetBool("isRolling", false);
        animator.SetTrigger("Attack");
        emitter.Play();
        if (YochiManager.instance.isInYokaiWorld)
        {
            emitter.rootBullet.moduleParameters.SetBool("isSpirit", true);
        }
        else
        {
            emitter.rootBullet.moduleParameters.SetBool("isSpirit", false);
        }


        yield return new WaitForSeconds(1.5f);
        isAttacking = false;
    }

    public void RotateEmitter()
    {
        Vector2 look = playerTransform.position - emitter.transform.position;

        if(look.normalized.x < 0)
        {
            animator.gameObject.GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);
        }
        else if (look.normalized.x > 0)
        {
            animator.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        emitter.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, look));
    }

    private float attackCDLeft;
    public void MoveAgent()
    {
        Vector2 distanceToPlayer = transform.position - playerTransform.position;

        if (attackCDLeft > 0)
        {
            attackCDLeft -= Time.deltaTime;
        }
        
        if (distanceToPlayer.magnitude > maxDistanceToPlayer)
        {
            if(!isAttacking)
            {
                targetPosition = YochiManager.instance.transform.position;
                animator.SetBool("isRolling", true);
                CalculatePath();
                UpdateDirection();
                rb.velocity = pathDirection * speed;
            }
        }
        else
        {
            animator.SetBool("isRolling", false);
            rb.velocity = Vector3.zero;

            if(attackCDLeft <= 0)
            {
                StartCoroutine(StartShooting());
            }
        }
    }
}
