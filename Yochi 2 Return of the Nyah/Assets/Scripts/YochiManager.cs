using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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
    public Volume postProcessVolume;
    public VolumeProfile yokaiWorldProfile;
    public VolumeProfile realWorldProfile;
    public float yokaiEffectLerpRatio;
    public GameObject yokaiEffectPrefab;

    [HideInInspector]
    public bool isInYokaiWorld;
    public static YochiManager instance;
    private YochiUmbrella yochiUmbrella;
    private BulletReceiver bulletReceiver;
    [HideInInspector]
    public bool isInvulnerable;
    [HideInInspector]
    public int currentHealthPoint;

    private VolumeProfile currentProfile;

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

        yokaiEffectCurrentState = 0;
        currentProfile = Instantiate(yokaiWorldProfile);

        postProcessVolume.sharedProfile = currentProfile;
    }

    void Update()
    {
        if (Input.GetButtonDown("RightBumper") || Input.GetButtonDown("LeftBumper") || Input.GetKeyDown(KeyCode.I))
        {
            SwitchWorld(!isInYokaiWorld);
        }
        InvulnerableTimeUpdate();
        UpdatePostProcess();
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

            Instantiate(yokaiEffectPrefab, transform.position, Quaternion.identity);
            //Debug.Log("Switch to Yokai world");
        }
        isInYokaiWorld = isYokaiWorld;
        yochiUmbrella.SwitchEmitter(isInYokaiWorld);
        animator.SetBool("isYokai", isInYokaiWorld);
        BulletManager.instance.OnChangeWorld();
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
                animator.SetTrigger("Hit");
                StartCoroutine(ShakeImpact());
                if (currentHealthPoint <= 0)
                {
                    Die();
                }
            }
        }
    }

    public void Die()
    {
        animator.SetTrigger("Die");
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

    public void getHP(int hp)
    {
        currentHealthPoint += hp;

        //feedback heal
    }

    private float lastRealTime;
    private float yokaiEffectCurrentState;
    private void UpdatePostProcess()
    {
        if (isInYokaiWorld)
        {
            yokaiEffectCurrentState = Mathf.Lerp(yokaiEffectCurrentState, 1, yokaiEffectLerpRatio * (Time.realtimeSinceStartup - lastRealTime));

        }
        else
        {
            if (yokaiEffectCurrentState < 0.01f)
            {
                yokaiEffectCurrentState = 0;
                //postProcessVolume.sharedProfile = realWorldProfile;
            }
            else
            {
                yokaiEffectCurrentState = Mathf.Lerp(yokaiEffectCurrentState, 0, yokaiEffectLerpRatio * (Time.realtimeSinceStartup - lastRealTime));
            }
        }

        postProcessVolume.weight = yokaiEffectCurrentState;
        lastRealTime = Time.realtimeSinceStartup;
    }
}
