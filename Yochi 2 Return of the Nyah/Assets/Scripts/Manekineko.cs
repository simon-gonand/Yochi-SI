using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manekineko : MonoBehaviour
{
    [SerializeField]
    private Transform self;

    [SerializeField]
    private float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        float newZRotation = self.eulerAngles.z + rotateSpeed * Time.deltaTime;
        self.rotation = Quaternion.Euler(0.0f, 0.0f, newZRotation);
    }
}