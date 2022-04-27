using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torii : MonoBehaviour
{
    public void OnHit(BulletPro.Bullet bullet, Vector3 position)
    {
        BulletManager.instance.ChangeBulletWorld(bullet);
    }
}
