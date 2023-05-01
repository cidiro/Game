using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject WinnerImage;  //Refferences to both GameObjects inside of the Panel gameObject in the end sceene. We reference them 
    [SerializeField] private GameObject LooserImage;  //so we can change the image of each one from here.
    
    //We want to, as soon as the End Sceene is accessed, have the images of the winner and looser, thats why we set them in the Start method.
    //We use the refferences to the images to access the sprites, and be able to change them. 
    private void Start()
    {
        WinnerImage.GetComponent<Image>().sprite = GameManager.GetWinnerSprite();

        LooserImage.GetComponent<Image>().sprite = GameManager.GetLooserSprite();
    }

    public void RestartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        GameManager.Replay();
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
