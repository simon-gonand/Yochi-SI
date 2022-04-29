using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip[] footstep;
    public AudioClip[] hit;
    public AudioClip death;
    public AudioClip mainMusic;

    public AudioSource audioSource;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        /*audioSource.clip = mainMusic;
        audioSource.Play();*/
    }

    public void PlayFoostep()
    {
        audioSource.PlayOneShot(footstep[Random.Range(0,3)]);
    }
    public void PlayHit()
    {
        audioSource.PlayOneShot(hit[Random.Range(0, 2)]);
    }

    public void PlayDeath()
    {
        audioSource.PlayOneShot(death);

    }
}
