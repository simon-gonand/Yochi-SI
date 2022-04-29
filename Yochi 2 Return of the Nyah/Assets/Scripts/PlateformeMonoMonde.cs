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
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
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
                boxCollider.enabled = false;
                spriteRenderer.color = new Color(1f,1f,1f,1f);
            }
            else
            {
                spriteRenderer.sprite = yokaiWorldInvisibleSprite;
                boxCollider.enabled = true;
                spriteRenderer.color = new Color(1f,1f,1f,0.2f);
            }
        }
        else
        {
            if (YochiManager.instance.isInYokaiWorld)
            {
                spriteRenderer.sprite = realWorldInvisibleSprite;
                boxCollider.enabled = true;
                spriteRenderer.color = new Color(1f,1f,1f,0.2f);
            }
            else
            {
                spriteRenderer.sprite = realWorldVisibleSprite;
                boxCollider.enabled = false;
                spriteRenderer.color = new Color(1f,1f,1f,1f);
            }
        }
        
    }
}
