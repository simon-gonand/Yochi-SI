using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;
using UnityEngine.UI;

public class YochiUmbrella : MonoBehaviour
{
    public Animator playerAnimator;
    public EmitterProfile yokaiEmitter;
    public EmitterProfile realEmitter;

    public BulletEmitter bulletEmitter;
    public BulletEmitter shotgunEmitter;
    public GameObject umbrella;
    public float aimCursorDistance;
    public Sprite[] realUmbrellaOrientations;
    public Sprite[] yokaiUmbrellaOrientations;
    public SpriteRenderer umbrellaRenderer;
    public string umbrellaFrontLayer;
    public string umbrellaBackLayer;
    public Vector2 minMaxForBackUmbrella;
    public SpriteRenderer playerRenderer;

    public float yokaiRechargeTime;
    public float yokaiBulletCadence;
    public int yokaiChargerBulletNumber;
    public float realRechargeTime;
    public Image rechargeBar;

    public float umbrellaLerpSpeed;
    public float yokaiShotKnockback;
    public float realShotKnockback;

    private Vector2 aimInput;
    private Vector2 aimDirection;
    private bool isShooting;
    private float umbrellaAngle;
    private float rechargeTimeRemaining;
    private float cadenceLeft;
    private int bulletLeft;
    private float umbrellaTargetDistance;

    void Start()
    {
        SwitchEmitter(false);
        umbrellaAngle = 90;

        bulletLeft = yokaiChargerBulletNumber;
    }

    void Update()
    {
        UpdateUmbrellaAim();
        UpdateUmbrellaOrientation();
        UpdateUmbrellaShootState();
    }

    private void YokaiSingleShoot()
    {
        bulletEmitter.Play();
        bulletEmitter.Play();
        KnockBackUmbrella(yokaiShotKnockback);

        cadenceLeft = yokaiBulletCadence;
        bulletLeft--;
        if (bulletLeft <= 0)
        {
            rechargeTimeRemaining = yokaiRechargeTime;
            bulletLeft = yokaiChargerBulletNumber;
        }
    }

    private void RealBigShoot()
    {
        bulletEmitter.Play();
        bulletEmitter.Play();
        shotgunEmitter.Play();
        rechargeTimeRemaining = realRechargeTime;
        bulletLeft = yokaiChargerBulletNumber;
        KnockBackUmbrella(realShotKnockback);
    }

    private void UpdateUmbrellaShootState()
    {
        if (isShooting)
        {
            if (YochiManager.instance.isInYokaiWorld)
            {
                if (rechargeTimeRemaining <= 0)
                {
                    if (cadenceLeft > 0)
                    {
                        cadenceLeft -= Time.deltaTime;
                    }
                    else
                    {
                        YokaiSingleShoot();
                    }
                }
            }
        }

        if (Input.GetAxis("RightTrigger") == 1 || Input.GetKeyDown(KeyCode.K)/* || aimInput.magnitude > 0.3f*/)
        {
            if (!isShooting)
            {
                isShooting = true;

                if (rechargeTimeRemaining <= 0)
                {
                    if(YochiManager.instance.isInYokaiWorld)
                    {
                        YokaiSingleShoot();
                    }
                    else
                    {
                        RealBigShoot();
                    }
                }
            }
        }
        else
        {
            if(isShooting)
            {
                isShooting = false;
            }
        }

        if(YochiManager.instance.isInYokaiWorld)
        {

            if (rechargeTimeRemaining > 0)
            {
                //rechargeBar.fillAmount = (yokaiRechargeTime - rechargeTimeRemaining) / yokaiRechargeTime;
                rechargeTimeRemaining -= Time.deltaTime;
                //rechargeBar.gameObject.SetActive(true);
            }
            else
            {
                //rechargeBar.fillAmount = (float)bulletLeft / (float)yokaiChargerBulletNumber;
                //rechargeBar.gameObject.SetActive(true);
            }
        }
        else
        {
            //rechargeBar.fillAmount = (realRechargeTime - rechargeTimeRemaining) / realRechargeTime;
            if (rechargeTimeRemaining > 0)
            {
                rechargeTimeRemaining -= Time.deltaTime;
                //rechargeBar.gameObject.SetActive(true);
            }
            else
            {
                //rechargeBar.gameObject.SetActive(false);
            }
        }
    }

    public void SwitchEmitter(bool isYokai)
    {
        bulletEmitter.Stop();
        isShooting = false;
        if (isYokai)
        {
            bulletEmitter.SwitchProfile(yokaiEmitter, false, PlayOptions.DoNothing);
        }
        else
        {
            bulletEmitter.SwitchProfile(realEmitter, false, PlayOptions.DoNothing);
        }
    }

    private float currentUmbrellaDistance;
    private void UpdateUmbrellaAim()
    {
        aimInput = new Vector2(Input.GetAxis("RightStickH"), Input.GetAxis("RightStickV"));


        if (aimInput.magnitude > 0.3f)
        {
            aimDirection = aimInput.normalized;
            umbrellaAngle = Vector2.SignedAngle(Vector2.right, aimDirection);


            bulletEmitter.transform.rotation = Quaternion.Euler(0, 0, umbrellaAngle - 90);
            shotgunEmitter.transform.rotation = Quaternion.Euler(0, 0, umbrellaAngle - 90);
        }

        currentUmbrellaDistance -= currentUmbrellaDistance * umbrellaLerpSpeed * Time.deltaTime;
        umbrella.transform.localPosition = (aimCursorDistance + currentUmbrellaDistance) * aimDirection;
    }

    private void KnockBackUmbrella(float distance)
    {
        currentUmbrellaDistance -= distance;
    }

    private void UpdateUmbrellaOrientation()
    {
        int indexOrientation = 0;
        while((umbrellaAngle >= 0 ? umbrellaAngle : (umbrellaAngle + 360)) > (22.5f + (indexOrientation * 45f)))
        {
            indexOrientation++;
        }

        if(YochiManager.instance.isInYokaiWorld)
        {
            umbrellaRenderer.sprite = yokaiUmbrellaOrientations[indexOrientation];
        }
        else
        {
            umbrellaRenderer.sprite = realUmbrellaOrientations[indexOrientation];
        }

        if(umbrellaAngle > minMaxForBackUmbrella.x && umbrellaAngle < minMaxForBackUmbrella.y)
        {
            umbrellaRenderer.sortingLayerID = SortingLayer.NameToID(umbrellaBackLayer);
        }
        else
        {
            umbrellaRenderer.sortingLayerID = SortingLayer.NameToID(umbrellaFrontLayer);
        }

        playerAnimator.SetFloat("AimAngle", umbrellaAngle);

        playerRenderer.flipX = umbrellaAngle > 90 || umbrellaAngle < -90;
    }
}
