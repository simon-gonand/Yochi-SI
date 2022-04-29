using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;

public class Manekineko : MonoBehaviour
{
    [SerializeField]
    private Transform self;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private BulletEmitter emitter;

    [SerializeField]
    private int rotateStep = 0;
    private float timer;
    [SerializeField] private float shootDelay;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= shootDelay)
        {
            timer = 0;

            animator.SetInteger("Orientation", rotateStep);

            float newZRotation = (rotateStep * 45) + 180;
            self.rotation = Quaternion.Euler(0.0f, 0.0f, -newZRotation);

            emitter.Play();

            rotateStep++;

            if (rotateStep > 7)
            {
                rotateStep = 0;
            }
        }
    }
}