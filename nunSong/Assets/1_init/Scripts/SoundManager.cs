using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource audioSource;
    public AudioClip uiClick;

    void Awake()
    {
        Instance = this;
    }

    public void PlayUIClick()
    {
        audioSource.PlayOneShot(uiClick);
    }
}
