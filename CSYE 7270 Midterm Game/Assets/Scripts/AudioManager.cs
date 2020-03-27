using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AudioManager: MonoBehaviour
{

    public AudioClip[] PlayerVoice;
    public AudioClip[] EnemyVoice;
    public AudioClip[] ExplosionClip;
    public AudioClip[] gunClip;
    public AudioSource srcPlayer;
    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }


    public void PlayerEnemyVoice(AudioSource source, int index = 0)
    {
        index = 0;
        // Check Range
        source.clip = EnemyVoice[index];
        source.Play();
    }

    public void PlayPlayerVoice(int index = 0)
    {
        index = 0;
        // Check Range
        srcPlayer.clip = PlayerVoice[index];
        srcPlayer.Play();
    }


    public void PlayExplosionSFX(AudioSource src,int index = 0)
    {
        Debug.Log("Play The Explosion!!!!!!!!!");
        src.clip = ExplosionClip[index];
        src.Play();
    }

    public void PlayReloadSFX(AudioSource src, int index = 0)
    {
        src.clip = gunClip[index];
        src.Play();
    }
}
