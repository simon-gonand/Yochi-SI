using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;
using UnityEngine.UI;
using Cinemachine;

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
    public Animator animator;
    public CinemachineVirtualCamera cineCamera;
    public float shakeIntensity;
    public float shakeTime;

    [HideInInspector]
    public bool isInYokaiWorld;
    public static YochiManager instance;
    private YochiUmbrella yochiUmbrella;
    private BulletReceiver bulletReceiver;
    [HideInInspector]
    public bool isInvulnerable;
    private int currentHealthPoint;

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
            //Debug.Log("Switch to Real world");
        }
        else
        {
            spriteRenderer.color = yokaiWorldColor;
            bulletReceiver.collisionTags = collisionInYokaiWorld;
            //Debug.Log("Switch to Yokai world");
        }
        isInYokaiWorld = isYokaiWorld;
        yochiUmbrella.SwitchEmitter(isInYokaiWorld);
        animator.SetBool("isYokai", isInYokaiWorld);
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
            StartCoroutine(ShakeImpact());
            if (currentHealthPoint > 0)
            {
                currentHealthPoint--;
                //feedback dégats
            }
        }
    }

    public IEnumerator ShakeImpact()
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        float currentIntensity = shakeIntensity;
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = shakeIntensity;
        while(currentIntensity > 0)
        {
            currentIntensity -= Time.deltaTime * shakeIntensity / shakeTime;
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = currentIntensity;
            yield return new WaitForEndOfFrame();
        }

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
    }
}
