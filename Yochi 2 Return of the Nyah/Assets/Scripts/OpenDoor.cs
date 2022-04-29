using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D col;

    private void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    public void Open()
    {
        anim.SetBool("open", true);
        col.enabled = false;
    }

    public void Close()
    {
        anim.SetBool("open", false);
        col.enabled = true;
    }
}
