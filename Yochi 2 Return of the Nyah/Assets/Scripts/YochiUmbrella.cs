using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YochiUmbrella : MonoBehaviour
{
    public GameObject umbrellaAim;
    public float aimCursorDistance;

    private Vector2 aimDirection;
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

    }
}
