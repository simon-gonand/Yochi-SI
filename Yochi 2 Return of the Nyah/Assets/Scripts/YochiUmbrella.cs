using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;

public class YochiUmbrella : MonoBehaviour
{
    public EmitterProfile yokaiEmitter;
    public EmitterProfile realEmitter;

    public BulletEmitter bulletEmitter;
    public GameObject umbrella;
    public float aimCursorDistance;
    public Sprite[] realUmbrellaOrientations;
    public Sprite[] yokaiUmbrellaOrientations;
    public SpriteRenderer umbrellaRenderer;

    private Vector2 aimInput;
    private Vector2 aimDirection;
    private bool isShooting;
    private float umbrellaAngle;

    void Start()
    {
        SwitchEmitter(false);
    }

    void Update()
    {
        UpdateInput();
        UpdateUmbrellaOrientation();
    }

    void Shoot()
    {
        if(!isShooting)
        {
            isShooting = true;
            bulletEmitter.Play();
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

    private void UpdateInput()
    {
        aimInput = new Vector2(Input.GetAxis("RightStickH"), Input.GetAxis("RightStickV"));


        if (aimInput.magnitude > 0.3f)
        {
            aimDirection = aimInput.normalized;
            umbrellaAngle = Vector2.SignedAngle(Vector2.right, aimDirection);

            umbrella.transform.localPosition = aimCursorDistance * aimDirection;
            bulletEmitter.transform.rotation = Quaternion.Euler(0, 0, umbrellaAngle - 90);
        }

        if (Input.GetAxis("RightTrigger") == 1)
        {
            Shoot();
        }
        else
        {
            isShooting = false;

            bulletEmitter.Stop();
        }
    }

    private void UpdateUmbrellaOrientation()
    {
        int indexOrientation = 0;
        while(umbrellaAngle > (22.5f + (indexOrientation * 45f)))
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
    }
}
