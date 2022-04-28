using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    public int lifePoints;
    public SpriteRenderer render;

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

    public IEnumerator HitFeedback()
    {
        render.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        render.color = Color.white;
    }

}
