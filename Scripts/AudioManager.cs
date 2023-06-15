using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource First;
    public AudioSource Second;
    public AudioSource Third;
    public static bool IsPlaying;
    public static int Index;

    public void Awake()
    {
        IsPlaying = false;
        Index = 0;
    }

    public void PlayClip()
    {
        if (IsPlaying)
        {
            IsPlaying = false;
            Index = (Index + 1) % 3;
            First.enabled = false;
            Second.enabled = false;
            Third.enabled = false;
        }
        else
        {
            IsPlaying = true;
            switch (Index)
            {
                case 0:
                    First.enabled = true;
                    Second.enabled = false;
                    Third.enabled = false;
                    break;
                case 1:
                    First.enabled = false;
                    Second.enabled = true;
                    Third.enabled = false;
                    break;
                case 2:
                    First.enabled = false;
                    Second.enabled = false;
                    Third.enabled = true;
                    break;
            }
        }
    }
}
