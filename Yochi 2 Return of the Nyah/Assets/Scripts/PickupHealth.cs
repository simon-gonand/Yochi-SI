using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupHealth : MonoBehaviour
{
    private Collider2D collider;
    public int numberOfHPHeal;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<YochiManager>().getHP(numberOfHPHeal);
            Destroy(gameObject);
        }
    }
    
    
}
