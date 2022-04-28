using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    public int lifePoints;
    public Seeker seeker;

    protected Transform playerTransform;

    private Path path;
    private int currentWaypoint;
    public bool pathEndReached;
    private float nextWaypointDistance;
    public Vector2 pathDirection;

    private void Start()
    {
        playerTransform = YochiManager.instance.transform;
    }

    public void HitByBullet()
    {
        lifePoints -= 1;
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
        seeker.StartPath(transform.position, playerTransform.position, OnPathComplete);
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
}
