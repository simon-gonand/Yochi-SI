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
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthPoint = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (YochiManager.instance.isInYokaiWorld)
        {
            spriteRenderer.sprite = yokaiWall;
            spriteRenderer.color = Color.blue;
        }
        else
        {
            spriteRenderer.sprite = realWall;
            spriteRenderer.color = Color.red;
        }
    }

    public void OnTakeDamage(BulletPro.Bullet bullet, Vector3 vec3)
    {
        if (canTakeDamage)
        {
            healthPoint -= 1;
        }

        if (healthPoint <= 0)
        {
            Destroy(this);
        }
    }
}
