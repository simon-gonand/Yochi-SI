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
    private bool canMove = true;
    public Animator animator;


    void Update()
    {
        RotateEmitter();

        MoveAgent();
    }

    public void HitByBullet(int dmg)
    {
        lifePoints -= dmg;
        if (lifePoints <= 0)
        {
            //death VFX
            Destroy(transform.parent);
        }
    }

    public IEnumerator StartShooting()
    {
        emitter.Play();
        if (YochiManager.instance.isInYokaiWorld)
        {
            emitter.rootBullet.moduleParameters.SetBool("isSpirit", true);
        }
        else
            emitter.rootBullet.moduleParameters.SetBool("isSpirit", false);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isRolling", false);
        animator.SetBool("isAttacking", false);
        canMove = true;
    }

    public void RotateEmitter()
    {
        Vector2 look = playerTransform.position - emitter.transform.position;

        if(look.normalized.x < 0)
        {
            animator.gameObject.GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);
        }
        emitter.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, look));
    }

    public void MoveAgent()
    {
        Vector2 distanceToPlayer = transform.position - playerTransform.position;

        if (distanceToPlayer.magnitude <= minDistanceToPlayer && canMove)
        {
            canMove = false;
            animator.SetBool("isAttacking", true);
            animator.SetBool("isRolling", false);
            GetComponentInParent<Rigidbody2D>().velocity = Vector3.zero;
            StartCoroutine(StartShooting());
        }

        else if (distanceToPlayer.magnitude > maxDistanceToPlayer && canMove)
        {
            
            animator.SetBool("isRolling", true);
            //GetComponentInParent<Rigidbody2D>().velocity = -distanceToPlayer.normalized * speed;
            CalculatePath();
            UpdateDirection();
            GetComponentInParent<Rigidbody2D>().velocity = GetComponent<EnemyParent>().pathDirection * speed;
        }
    }
}
