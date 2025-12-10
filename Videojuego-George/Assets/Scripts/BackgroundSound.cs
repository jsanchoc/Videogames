using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    [Header("---------- src --------------")]
    [SerializeField] AudioSource audioSource;

    [Header("---------- bck --------------")]
    public AudioClip background;

    private void Start()
    {
        audioSource.clip = background;
        audioSource.Play();
    }

}
