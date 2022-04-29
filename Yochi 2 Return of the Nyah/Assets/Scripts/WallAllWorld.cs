using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAllWorld : MonoBehaviour
{
    public Sprite yokaiWorldSprite;
    public Sprite realWorldSprite;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (YochiManager.instance.isInYokaiWorld)
        {
            spriteRenderer.sprite = yokaiWorldSprite;
        }
        else
        {
            spriteRenderer.sprite = realWorldSprite;
        }
    }
}
