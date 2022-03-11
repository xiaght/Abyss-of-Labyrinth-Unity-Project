using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip homebgm;
    public AudioClip dungeonbgm;
    public AudioClip bossbgm;

    public GameManager gm;


    public void PlayHomeSound()
    {
        audio.clip = homebgm;
        audio.Play();

    }

    public void PlayDungeonSound()
    {
        audio.clip = dungeonbgm;
        audio.Play();

    }
    public void PlayBossSound()
    {
        audio.clip = bossbgm;
        audio.Play();

    }

}
