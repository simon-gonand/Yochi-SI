using System.Collections;
using System.Collections.Generic;
using BulletPro;
using UnityEngine;

public class MurMonoMonde : MonoBehaviour
{
    public Sprite yokaiWorldVisibleSprite;
    public Sprite realWorldVisibleSprite;
    public Sprite yokaiWorldInvisibleSprite;
    public Sprite realWorldInvisibleSprite;
    public bool isForYokaiWorld;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider;
    private BulletReceiver receiver;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        receiver = GetComponent<BulletReceiver>();
        ChangeSprite();

    }

    // Update is called once per frame
    void Update()
    {
        ChangeSprite();
    }

    void ChangeSprite()
    {
        if (isForYokaiWorld)
        {
            if (YochiManager.instance.isInYokaiWorld)
            {
                spriteRenderer.sprite = yokaiWorldVisibleSprite;
                collider.enabled = true;
                receiver.enabled = true;
                spriteRenderer.color = Color.blue;
            }
            else
            {
                spriteRenderer.sprite = yokaiWorldInvisibleSprite;
                collider.enabled = false;
                receiver.enabled = false;
                spriteRenderer.color = Color.red;
            }
        }
        else
        {
            if (YochiManager.instance.isInYokaiWorld)
            {
                spriteRenderer.sprite = realWorldInvisibleSprite;
                collider.enabled = false;
                receiver.enabled = false;
                spriteRenderer.color = Color.red;
            }
            else
            {
                spriteRenderer.sprite = realWorldVisibleSprite;
                collider.enabled = true;
                receiver.enabled = true;
                spriteRenderer.color = Color.blue;
            }
        }
    }
}
