using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip[] hoverClipList;
    public AudioClip[] clickClipList;

    public AudioSource menu_audio_src;
    public static MenuAudioManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        if (menu_audio_src == null)
            menu_audio_src = GetComponent<AudioSource>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayButtonHoverSFX(int index = 0)
    {
        menu_audio_src.clip = hoverClipList[index];
        menu_audio_src.Play();
    }


    public void PlayButtonClickSFX(int index = 0)
    {
        menu_audio_src.clip = clickClipList[index];
        menu_audio_src.Play();
    }

}
