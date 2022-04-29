using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurYokai : MonoBehaviour
{
    public Sprite realWall;
    public Sprite yokaiWall;
    private SpriteRenderer spriteRenderer;
    public int healthPoint;
    private bool canTakeDamage;
    private Animator animator;
    private BoxCollider2D col;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ResetHealth();
        animator = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    public void ResetHealth()
    {
        healthPoint = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (YochiManager.instance.isInYokaiWorld)
        {
            //spriteRenderer.sprite = yokaiWall;
            animator.enabled = true;
            //spriteRenderer.color = Color.blue;
            canTakeDamage = true;
        }
        else
        {
            animator.enabled = false;
            spriteRenderer.sprite = realWall;
            //spriteRenderer.color = Color.red;
            canTakeDamage = false;
        }
    }

    public void OnTakeDamage(BulletPro.Bullet bullet, Vector3 vec3)
    {
        if (canTakeDamage)
        {
            healthPoint -= 1;
            StartCoroutine(HitFeedback());
        }

        if (healthPoint <= 0)
        {
            GameManager.instance.currentRoom.destroyedGameObject.Add(this);
            StartCoroutine(WallDeath());
            col.enabled = false;
        }
    }

    public IEnumerator HitFeedback()
    {
        animator.SetTrigger("Hit");
        yield return new WaitForSeconds(0.2f);
    }


    public IEnumerator WallDeath()
    {
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1.6f);
        gameObject.SetActive(false);
    }
}
