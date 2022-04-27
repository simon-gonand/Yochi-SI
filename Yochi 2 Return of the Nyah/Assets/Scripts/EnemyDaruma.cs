using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;

public class EnemyDaruma : MonoBehaviour
{
    public float speed;
    public float minDistanceToPlayer;
    public float maxDistanceToPlayer;
    //public float timeBewteenShots;
    public BulletEmitter emitter;
    public int lifePoints;
    private Transform playerPos;
    //public bool canShoot = true;
    private bool canMove = true;
    public Animator animator;


    void Start()
    {
        playerPos = YochiManager.instance.transform;
    }


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
        //emitter.Stop();
        canMove = true;
    }

    public void RotateEmitter()
    {
        Vector2 look = playerPos.position - emitter.transform.position;
        emitter.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, look));
    }

    public void MoveAgent()
    {
        Vector2 distanceToPlayer = transform.position - playerPos.position;

        if (distanceToPlayer.magnitude <= minDistanceToPlayer)
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
            GetComponentInParent<Rigidbody2D>().velocity = -distanceToPlayer.normalized * speed;
            //emitter.Stop();
            //canShoot = true;
            //isMoving = true;
        }

        /*else
        {
            if (isMoving)
            {
                GetComponentInParent<Rigidbody2D>().velocity = Vector3.zero;
                isMoving = false;
                StartShooting();
            }

        }*/
    }
}
