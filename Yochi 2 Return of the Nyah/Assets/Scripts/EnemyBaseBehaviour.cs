using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;

public class EnemyBaseBehaviour : MonoBehaviour
{
    public float speed;
    public float minDistanceToPlayer;
    public float maxDistanceToPlayer;
    //public float timeBewteenShots;
    public BulletEmitter emitter;
    public int lifePoints;
    private Transform playerPos;    
    public bool canShoot = true;
    private bool isMoving;


    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {        
        RotateEmitter();

        MoveAgent();
    }

    public void HitByBullet(int dmg)
    {
        lifePoints -= dmg;
        if(lifePoints <= 0)
        {
            //death VFX
            Destroy(transform.parent.gameObject);
        }
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
        Vector2 look = playerPos.position - emitter.transform.position;
        emitter.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, look));
    }

    public void MoveAgent()
    {
        Vector2 distanceToPlayer = transform.position - playerPos.position;

        if (distanceToPlayer.magnitude < minDistanceToPlayer)
        {
            GetComponentInParent<Rigidbody2D>().velocity = distanceToPlayer.normalized * speed;
            StartShooting();
            isMoving = true;
        }

        else if (distanceToPlayer.magnitude > maxDistanceToPlayer)
        {
            GetComponentInParent<Rigidbody2D>().velocity = -distanceToPlayer.normalized * speed;
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
