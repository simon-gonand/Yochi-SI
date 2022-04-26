using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;

public class YochiUmbrella : MonoBehaviour
{
    public BulletEmitter bulletEmitter;
    public GameObject umbrellaAim;
    public float aimCursorDistance;

    private Vector2 aimDirection;

    private bool isShooting;

    void Start()
    {
        
    }

    void Update()
    {
        aimDirection = new Vector2(Input.GetAxis("RightStickH"), Input.GetAxis("RightStickV"));


        if (aimDirection.magnitude > 0)
        {
            aimDirection.Normalize();

            umbrellaAim.SetActive(true);
            umbrellaAim.transform.localPosition = aimCursorDistance * aimDirection;
        }
        else
        {
            umbrellaAim.SetActive(false);
        }

        if(Input.GetAxis("RightTrigger") == 1)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if(!isShooting)
        {
            isShooting = true;
            bulletEmitter.Play();
        }
        else
        {
            isShooting = false;
        }
    }
}
