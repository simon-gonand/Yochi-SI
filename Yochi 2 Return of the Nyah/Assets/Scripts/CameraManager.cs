using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject player;

    public float lerpSpeed;

    private Vector2 targetPosition;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }


    void Update()
    {
    }

    private void FixedUpdate()
    {
        targetPosition = player.transform.position;

        mainCamera.transform.position = Vector2.Lerp(mainCamera.transform.position, targetPosition, lerpSpeed * Time.fixedDeltaTime);
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -10);
    }
}
