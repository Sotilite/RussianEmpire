using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource First;
    public static bool IsPlaying;

    public void Awake()
    {
        IsPlaying = false;
    }

    public void PlayClip()
    {
        if (IsPlaying)
        {
            IsPlaying = false;
            First.enabled = false;
        }
        else
        {
            IsPlaying = true;
            First.enabled = true;
        }
    }
}
