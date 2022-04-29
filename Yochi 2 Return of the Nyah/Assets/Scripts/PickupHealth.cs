using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupHealth : MonoBehaviour
{
    private Collider2D colliderComp;
    public int numberOfHPHeal;
    // Start is called before the first frame update
    void Start()
    {
        colliderComp = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (YochiManager.instance.currentHealthPoint == YochiManager.instance.maxHealthPoint) return;
            col.GetComponent<YochiManager>().getHP(numberOfHPHeal);
            Destroy(gameObject);
        }
    }
    
    
}
