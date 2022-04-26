using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;

public class YochiManager : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color realWorldColor;
    public Color yokaiWorldColor;
    [Header("Collision in yokai dimension")]
    public CollisionTags collisionInYokaiWorld;
    [Header("Collision in real dimension")]
    public CollisionTags collisionInRealWorld;

    [HideInInspector]
    public bool isInYokaiWorld;
    public static YochiManager instance;
    private YochiUmbrella yochiUmbrella;
    private BulletReceiver bulletReceiver;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        yochiUmbrella = GetComponent<YochiUmbrella>();
        bulletReceiver = GetComponent<BulletReceiver>();
        isInYokaiWorld = false;
        SwitchWorld(isInYokaiWorld);
    }

    void Update()
    {
        if (Input.GetButtonDown("RightBumper") || Input.GetButtonDown("LeftBumper"))
        {
            SwitchWorld(!isInYokaiWorld);
        }
    }

    public void SwitchWorld(bool isYokaiWorld)
    {
        if(!isYokaiWorld)
        {
            spriteRenderer.color = realWorldColor;
            bulletReceiver.collisionTags = collisionInRealWorld;
            Debug.Log("Switch to Real world");
        }
        else
        {
            spriteRenderer.color = yokaiWorldColor;
            bulletReceiver.collisionTags = collisionInYokaiWorld;
            Debug.Log("Switch to Yokai world");
        }
        isInYokaiWorld = isYokaiWorld;
        yochiUmbrella.SwitchEmitter(isInYokaiWorld);
    }
}
