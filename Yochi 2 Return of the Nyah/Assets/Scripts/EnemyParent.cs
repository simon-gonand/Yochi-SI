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

    protected Transform playerTransform;
    public Vector3 targetPosition;
    private ScoringManager scoringManager;

    private Path path;
    private int currentWaypoint;
    public bool pathEndReached;
    private float nextWaypointDistance;
    public Vector2 pathDirection;

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
    }

    public void HitByBullet()
    {
        lifePoints -= 1;
        StartCoroutine(HitFeedback());

        if (lifePoints <= 0)
        {
            StartCoroutine(DeathFX());
        }
    }

    public IEnumerator DeathFX()
    {
        yield return new WaitForSeconds(0.21f);
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
        render.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        render.color = Color.white;
    }

}
