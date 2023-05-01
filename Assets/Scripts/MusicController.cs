using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Not used right now but stays because it can be useful in the future, the idea is that it could control all the music and sounds from here.
//It would work by having the GameObject or the players call the methods in MusicController to play music, SFX, ...
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

    //Params: onlySound-> Boolean that will be true if we want this Music to be the only one sounding in the moment
    public void PlayFightMusic(bool onlySound){
        //If we want it to be the only music on the moment we dissable all the others just in case one was playing too.
        if(onlySound){
            menusMusic.Stop();
            endMusic.Stop();
        }
        fightMusic.Play();
    }

    //Params: onlySound-> Boolean that will be true if we want this Music to be the only one sounding in the moment
    public void PlayEndingMusic(bool onlySound){
        if(onlySound){
            menusMusic.Stop();
            fightMusic.Stop();
        }
        endMusic.Play();
    }
}
