using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    public EnemyTypes type;
    public int lifePoints;
    public Seeker seeker;
    public SpriteRenderer render;
    public Animator animator;
    public Animator yokaiAnimator;
    public SpriteRenderer realSprite;
    public SpriteRenderer yokaiSprite;

    protected Transform playerTransform;

    [HideInInspector]
    public Vector3 targetPosition;
    private ScoringManager scoringManager;

    private Path path;
    private int currentWaypoint;
    [HideInInspector]
    public bool pathEndReached;
    private float nextWaypointDistance;
    [HideInInspector]
    public Vector2 pathDirection;

    protected Rigidbody2D rb;

    public enum EnemyTypes
    {
        RealDoll,
        YokaiDoll,
        Kameosa,
        Chochin,
        Daruma
    }

    private void Start()
    {
        playerTransform = YochiManager.instance.transform;
        targetPosition = playerTransform.position;
        scoringManager = ScoringManager.instance;
        rb = GetComponentInParent<Rigidbody2D>();
    }

    public void HitByBullet()
    {
        lifePoints -= 1;
        StartCoroutine(HitFeedback());

        if (lifePoints <= 0)
        {
            GameManager.instance.currentRoom.OnEnemyDied(this);
            scoringManager.EnemyKilledScoring(type);
            StartCoroutine(DeathFX());
        }
    }

    public IEnumerator DeathFX()
    {
        animator.SetTrigger("Die");
        if(yokaiAnimator != null)
        {
            yokaiAnimator.SetTrigger("Die");
        }

        yield return new WaitForSeconds(0.6f);
        //Instantiate death VFX
        Destroy(transform.parent.gameObject);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    protected void CalculatePath()
    {
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
    }

    private float pathUpdateCDLeft;
    public float pathUpdateCooldown;
    protected void UpdatePath()
    {
        if(pathUpdateCDLeft <= 0)
        {
            CalculatePath();
            pathUpdateCDLeft = pathUpdateCooldown;
        }
        else
        {
            pathUpdateCDLeft -= Time.deltaTime;
        }
    }

    protected void UpdateDirection()
    {
        if (path != null)
        {
            if (currentWaypoint >= path.vectorPath.Count)
            {
                pathEndReached = true;
            }
            else
            {
                pathEndReached = false;

                while (Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance && path.vectorPath.Count - 1 > currentWaypoint + 1)
                {
                    currentWaypoint++;
                }

                pathDirection = (path.vectorPath[currentWaypoint + 1] - transform.position).normalized;
            }
        }
    }

    public IEnumerator HitFeedback()
    {
        animator.SetTrigger("Hit");

        if (yokaiAnimator != null)
        {
            yokaiAnimator.SetTrigger("Hit");
        }
        yield return new WaitForSeconds(0.2f);
    }

    protected void UpdateYokaiDisplay()
    {
        realSprite.enabled = !YochiManager.instance.isInYokaiWorld;
        yokaiSprite.enabled = YochiManager.instance.isInYokaiWorld;
    }

}
