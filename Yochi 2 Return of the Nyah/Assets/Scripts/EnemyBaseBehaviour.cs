using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;

public class EnemyBaseBehaviour : MonoBehaviour
{
    public float speed;
    public float minDistanceToPlayer;
    public float maxDistanceToPlayer;
    public float timeBewteenShots;
    public BulletEmitter emitter;
    public int lifePoints;
    private Transform playerPos;
    [HideInInspector] public bool canShoot;
    private bool isMoving;


    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {        
        RotateEmitter();

        MoveAgent();

        if (canShoot)
        {
            StartCoroutine(StartShooting());
        }
    }

    public void HitByBullet(int dmg)
    {
        lifePoints -= dmg;
        if(lifePoints <= 0)
        {
            Destroy(transform.parent);
        }
    }

    public IEnumerator StartShooting()
    {
        canShoot = false;
        emitter.Play();
        yield return new WaitUntil(() => emitter.isPlaying!);
        yield return new WaitForSeconds(timeBewteenShots);
        canShoot = true;
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
            Debug.Log("trop prêt");
            isMoving = true;
        }

        else if (distanceToPlayer.magnitude > maxDistanceToPlayer)
        {
            GetComponentInParent<Rigidbody2D>().velocity = -distanceToPlayer.normalized * speed;
            Debug.Log("trop loin");
            isMoving = true;
        }

        else
        {
            if (isMoving)
            {
                GetComponentInParent<Rigidbody2D>().velocity = Vector3.zero;
                isMoving = false;
                Debug.Log("nickel");
            }
            
        }
    }
}
