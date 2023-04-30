using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource menusMusic;
    [SerializeField] private AudioSource fightMusic;
    [SerializeField] private AudioSource endMusic;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //menusMusic.Play();
    }

    public void PlayFightMusic(bool onlySound){
        if(onlySound){
            menusMusic.Stop();
            endMusic.Stop();
        }
        fightMusic.Play();
    }

    public void PlayEndingMusic(bool onlySound){
        if(onlySound){
            menusMusic.Stop();
            fightMusic.Stop();
        }
        endMusic.Play();
    }
}
