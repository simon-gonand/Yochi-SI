using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    public int lifePoints;

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
}
