using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;

public class BulletManager
{
    private static BulletManager _instance;
    public static BulletManager instance
    {
        get
        {
            if (_instance is null)
                _instance = new BulletManager();
            return _instance;
        }
    }

    private List<Bullet> spiritBullet;
    private List<Bullet> realBullet;

    public void AddSpiritBullet(Bullet bullet)
    {
        spiritBullet.Add(bullet);
    }

    public void RemoveSpiritBullet(Bullet bullet)
    {
        spiritBullet.Remove(bullet);
    }

    public void AddRealBullet(Bullet bullet)
    {
        realBullet.Add(bullet);
    }

    public void RemoveRealBullet(Bullet bullet)
    {
        realBullet.Remove(bullet);
    }
}
