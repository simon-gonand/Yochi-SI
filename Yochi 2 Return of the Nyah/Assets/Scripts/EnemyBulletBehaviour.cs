using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;

// This script is supported by the BulletPro package for Unity.
// Template author : Simon Albou <albou.simon@gmail.com>

// This script is actually a MonoBehaviour for coding advanced things with Bullets.
public class EnemyBulletBehaviour : BaseBulletBehaviour {

	// You can access this.bullet to get the parent bullet script.
	// After bullet's death, you can delay this script's death : use this.lifetimeAfterBulletDeath.

	public Sprite yokaiBulletYokaiWorld;
	public Sprite yokaiBulletRealWorld;
	public Sprite realBulletYokaiWorld;
	public Sprite realBulletRealWorld;

	// Use this for initialization (instead of Start)
	public override void OnBulletBirth ()
	{
		base.OnBulletBirth();

		BulletManager.instance.AddEnemiesBullet(this.bullet);

		if (YochiManager.instance.isInYokaiWorld)
        {
			if (this.bullet.moduleCollision.collisionTags[6])
				this.bullet.spriteRenderer.sprite = ((EnemyBulletBehaviour)this.bullet.additionalBehaviourScripts[0]).yokaiBulletYokaiWorld;
			else if (this.bullet.moduleCollision.collisionTags[7])
				this.bullet.spriteRenderer.sprite = ((EnemyBulletBehaviour)this.bullet.additionalBehaviourScripts[0]).realBulletYokaiWorld;
			else
				Debug.LogWarning("One enemy bullet of " + this.bullet.emitter.name + " does not have collision tag");
		}
		else
        {
			if (this.bullet.moduleCollision.collisionTags[6])
				this.bullet.spriteRenderer.sprite = ((EnemyBulletBehaviour)this.bullet.additionalBehaviourScripts[0]).yokaiBulletRealWorld;
			else if (this.bullet.moduleCollision.collisionTags[7])
				this.bullet.spriteRenderer.sprite = ((EnemyBulletBehaviour)this.bullet.additionalBehaviourScripts[0]).realBulletRealWorld;
			else
				Debug.LogWarning("One enemy bullet of " + this.bullet.emitter.name + " does not have collision tag");
		}
	}
	
	// Update is (still) called once per frame
	public override void Update ()
	{
		base.Update();

		// Your code here
	}

	// This gets called when the bullet dies
	public override void OnBulletDeath()
	{
		base.OnBulletDeath();

		BulletManager.instance.RemoveEnemiesBullet(this.bullet);
	}

	// This gets called after the bullet has died, it can be delayed.
	public override void OnBehaviourDeath()
	{
		base.OnBehaviourDeath();

		// Your code here
	}

	// This gets called whenever the bullet collides with a BulletReceiver. The most common callback.
	public override void OnBulletCollision(BulletReceiver br, Vector3 collisionPoint)
	{
		base.OnBulletCollision(br, collisionPoint);

		// Your code here
	}

	// This gets called whenever the bullet collides with a BulletReceiver AND was not colliding during the previous frame.
	public override void OnBulletCollisionEnter(BulletReceiver br, Vector3 collisionPoint)
	{
		base.OnBulletCollisionEnter(br, collisionPoint);

		// Your code here
	}

	// This gets called whenever the bullet stops colliding with any BulletReceiver.
	public override void OnBulletCollisionExit()
	{
		base.OnBulletCollisionExit();

		// Your code here
	}

	// This gets called whenever the bullet shoots a pattern.
	public override void OnBulletShotAnotherBullet(int patternIndex)
	{
		base.OnBulletShotAnotherBullet(patternIndex);

		// Your code here
	}
}
