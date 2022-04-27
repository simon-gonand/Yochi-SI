using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;
using UnityEngine.UI;

public class YochiManager : MonoBehaviour
{
    public int maxHealthPoint;
    public float invulnerableTime;
    public Image healthBar;
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
    [HideInInspector]
    public bool isInvulnerable;
    public int currentHealthPoint;

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
        currentHealthPoint = maxHealthPoint;
    }

    void Update()
    {
        if (Input.GetButtonDown("RightBumper") || Input.GetButtonDown("LeftBumper"))
        {
            SwitchWorld(!isInYokaiWorld);
        }
        InvulnerableTimeUpdate();
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

    private void InvulnerableTimeUpdate()
    {
        if(invulTimeRemaining > 0)
        {
            invulTimeRemaining -= Time.deltaTime;
        }
        else
        {
            isInvulnerable = false;
        }
        healthBar.fillAmount = (float)currentHealthPoint / (float)maxHealthPoint;
    }

    private float invulTimeRemaining;
    public void TakeOneDamage()
    {
        if(!isInvulnerable)
        {
            isInvulnerable = true;
            invulTimeRemaining = invulnerableTime;
            if (currentHealthPoint > 0)
            {
                currentHealthPoint--;
                //feedback d�gats
            }
        }
    }

    public void getHP(int hp)
    {
        currentHealthPoint += hp;
    }
}
