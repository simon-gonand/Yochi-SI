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

    private List<Bullet> enemiesBullet = new List<Bullet>();

    public void AddEnemiesBullet(Bullet bullet)
    {
        enemiesBullet.Add(bullet);
    }

    public void RemoveEnemiesBullet(Bullet bullet)
    {
        enemiesBullet.Remove(bullet);
    }

    public void ChangeBulletWorld(Bullet bullet)
    {
        if (bullet.moduleCollision.collisionTags[6])
        {
            if (YochiManager.instance.isInYokaiWorld)
                bullet.spriteRenderer.sprite = ((EnemyBulletBehaviour)bullet.additionalBehaviourScripts[0]).realBulletYokaiWorld;
            else
                bullet.spriteRenderer.sprite = ((EnemyBulletBehaviour)bullet.additionalBehaviourScripts[0]).realBulletRealWorld;

            bullet.moduleCollision.collisionTags[6] = false;
            bullet.moduleCollision.collisionTags[7] = true;
        }
        else if (bullet.moduleCollision.collisionTags[7])
        {
            if (YochiManager.instance.isInYokaiWorld)
                bullet.spriteRenderer.sprite = ((EnemyBulletBehaviour)bullet.additionalBehaviourScripts[0]).yokaiBulletYokaiWorld;
            else
                bullet.spriteRenderer.sprite = ((EnemyBulletBehaviour)bullet.additionalBehaviourScripts[0]).yokaiBulletRealWorld;

            bullet.moduleCollision.collisionTags[6] = true;
            bullet.moduleCollision.collisionTags[7] = false;
        }
        else
            Debug.LogWarning("One enemy bullet of " + bullet.emitter.name + " does not have collision tag");
    }

    public void OnChangeWorld()
    {
        foreach(Bullet bullet in enemiesBullet)
        {
            if (YochiManager.instance.isInYokaiWorld)
            {
                if (bullet.moduleCollision.collisionTags[6])
                    bullet.spriteRenderer.sprite = ((EnemyBulletBehaviour)bullet.additionalBehaviourScripts[0]).yokaiBulletYokaiWorld;
                else if (bullet.moduleCollision.collisionTags[7])
                    bullet.spriteRenderer.sprite = ((EnemyBulletBehaviour)bullet.additionalBehaviourScripts[0]).realBulletYokaiWorld;
                else
                    Debug.LogWarning("One enemy bullet of " + bullet.emitter.name + " does not have collision tag");
            }
            else
            {
                if (bullet.moduleCollision.collisionTags[6])
                    bullet.spriteRenderer.sprite = ((EnemyBulletBehaviour)bullet.additionalBehaviourScripts[0]).yokaiBulletRealWorld;
                else if (bullet.moduleCollision.collisionTags[7])
                    bullet.spriteRenderer.sprite = ((EnemyBulletBehaviour)bullet.additionalBehaviourScripts[0]).realBulletRealWorld;
                else
                    Debug.LogWarning("One enemy bullet of " + bullet.emitter.name + " does not have collision tag");
            }
        }
    }
}
