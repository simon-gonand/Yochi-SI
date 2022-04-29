using System.Collections;
using System.Collections.Generic;
using BulletPro;
using UnityEngine;

public class PropsDestructible : MonoBehaviour
{
    public int chanceToDrop;
    public int rangeToDrop;
    public GameObject pickupHP;
    private BulletReceiver receiver;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destruction()
    {
        var rand = Random.Range(0, rangeToDrop);
        
        if (rand <= chanceToDrop)
        {
            Instantiate(pickupHP, this.transform.position, Quaternion.identity);
        }
        animator.SetBool("isDead", true);
        receiver.enabled = false;
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {        
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
