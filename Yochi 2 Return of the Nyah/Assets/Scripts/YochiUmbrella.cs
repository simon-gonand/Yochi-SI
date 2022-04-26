using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;

public class YochiUmbrella : MonoBehaviour
{
    public BulletEmitter bulletEmitter;
    public GameObject umbrella;
    public float aimCursorDistance;

    private Vector2 aimInput;
    private Vector2 aimDirection;

    private bool isShooting;

    void Start()
    {
        
    }

    void Update()
    {
        aimInput = new Vector2(Input.GetAxis("RightStickH"), Input.GetAxis("RightStickV"));


        if (aimInput.magnitude > 0.3f)
        {
            aimDirection = aimInput.normalized;

            umbrella.transform.localPosition = aimCursorDistance * aimDirection;
            bulletEmitter.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, aimDirection));
        }

        if(Input.GetAxis("RightTrigger") == 1)
        {
            Shoot();
        }
        else
        {
            isShooting = false;

            bulletEmitter.Stop();
        }
    }

    void Shoot()
    {
        if(!isShooting)
        {
            isShooting = true;
            bulletEmitter.Play();
        }
    }
}
