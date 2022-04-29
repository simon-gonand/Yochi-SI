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
        receiver = GetComponent<BulletReceiver>();
    }

    public void Reset()
    {
        receiver.enabled = true;
        animator.SetBool("isDead", false);
    }

    public void Destruction()
    {
        var rand = Random.Range(0, rangeToDrop);
        receiver.enabled = false;
        
        if (rand <= chanceToDrop)
        {
            Instantiate(pickupHP, this.transform.position, Quaternion.identity);
        }
        animator.SetBool("isDead", true);

        GameManager.instance.currentRoom.destroyedProps.Add(this);
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {        
        yield return new WaitForSeconds(0.6f);
        gameObject.SetActive(false);
    }
}
