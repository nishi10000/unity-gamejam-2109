using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource se_start, se_result, se_pouring, se_endPour;

    public void Play()
    {
        se_start.Play();
    }

    public void Result()
    {
        se_result.Play();
    }

    public void StartPour()
    {
        se_pouring.Play();
    }

    public void StopPour()
    {
        se_pouring.Stop();
        se_endPour.Play();
    }

}
