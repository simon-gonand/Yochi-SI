using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateformeMonoMonde : MonoBehaviour
{
    public Sprite yokaiWorldVisibleSprite;
    public Sprite realWorldVisibleSprite;
    public Sprite yokaiWorldInvisibleSprite;
    public Sprite realWorldInvisibleSprite;
    public bool isForYokaiWorld;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        ChangeSprite();

    }

    // Update is called once per frame
    void Update()
    {
        ChangeSprite();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //tomber + perte de PV
            Debug.Log("je tombe");
        } 
    }

    void ChangeSprite()
    {
        if (isForYokaiWorld)
        {
            if (YochiManager.instance.isInYokaiWorld)
            {
                spriteRenderer.sprite = yokaiWorldVisibleSprite;
                collider.enabled = false;
                spriteRenderer.color = Color.blue;
            }
            else
            {
                spriteRenderer.sprite = yokaiWorldInvisibleSprite;
                collider.enabled = true;
                spriteRenderer.color = Color.red;
            }
        }
        else
        {
            if (YochiManager.instance.isInYokaiWorld)
            {
                spriteRenderer.sprite = realWorldInvisibleSprite;
                collider.enabled = true;
                spriteRenderer.color = Color.green;
            }
            else
            {
                spriteRenderer.sprite = realWorldVisibleSprite;
                collider.enabled = false;
                spriteRenderer.color = Color.yellow;
            }
        }
        
    }
}
