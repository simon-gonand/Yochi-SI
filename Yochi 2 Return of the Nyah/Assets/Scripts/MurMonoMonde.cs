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
    private BoxCollider2D boxCollider;
    private BulletReceiver receiver;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
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
                boxCollider.enabled = true;
                receiver.enabled = true;
                spriteRenderer.color = Color.blue;
            }
            else
            {
                spriteRenderer.sprite = yokaiWorldInvisibleSprite;
                boxCollider.enabled = false;
                receiver.enabled = false;
                spriteRenderer.color = Color.red;
            }
        }
        else
        {
            if (YochiManager.instance.isInYokaiWorld)
            {
                spriteRenderer.sprite = realWorldInvisibleSprite;
                boxCollider.enabled = false;
                receiver.enabled = false;
                spriteRenderer.color = Color.green;
            }
            else
            {
                spriteRenderer.sprite = realWorldVisibleSprite;
                boxCollider.enabled = true;
                receiver.enabled = true;
                spriteRenderer.color = Color.yellow;
            }
        }
    }
}
