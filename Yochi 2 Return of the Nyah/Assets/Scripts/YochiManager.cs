using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YochiManager : MonoBehaviour
{
    [HideInInspector]
    public bool isInYokaiWorld;

    void Start()
    {
        isInYokaiWorld = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("RightBumper") || Input.GetButtonDown("LeftBumper"))
        {
            SwitchWorld();
        }
    }

    public void SwitchWorld()
    {
        if(isInYokaiWorld)
        {
            isInYokaiWorld = false;
            Debug.Log("Switch to Real world");
        }
        else
        {
            isInYokaiWorld = true;
            Debug.Log("Switch to Yokai world");
        }
    }
}
