using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    private void Awake()
    {  instance = this;  }

    [HideInInspector]public AudioSource bgmAS;
    public AudioClip[] bgms;
    private bool isGameOver;

    public AudioSource[] sfxSound;
    
    void Start()
    {
        bgmAS = transform.Find("BGM").GetComponent<AudioSource>();
        bgmAS.clip = bgms[0];
        bgmAS.volume = 0.1f;
        bgmAS.Play();
    }

    void Update()
    {
        if (UIController.instance.endPanel.activeSelf==true)
        {
            if (!isGameOver) 
            {
                bgmAS.clip = bgms[1];
                bgmAS.volume = 0.25f;
                bgmAS.Play();
                isGameOver = true;
            }
        }
    }

    public void PlaySFX(int id) 
    {
        sfxSound[id].Stop();
        sfxSound[id].Play();
    }
    public void PlaySFXPiched(int id) 
    {
        sfxSound[id].pitch=Random.Range(0.88f, 1.12f);
        PlaySFX(id);
    }
}
