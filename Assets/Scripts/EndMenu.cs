using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject WinnerImage;  //Refferences to both GameObjects inside of the Panel gameObject in the end sceene. We reference them 
    [SerializeField] private GameObject LooserImage;  //so we can change the image of each one from here.
    [SerializeField] private TextMeshProUGUI text;
    
    //We want to, as soon as the End Sceene is accessed, have the images of the winner and looser, thats why we set them in the Start method.
    //We use the refferences to the images to access the sprites, and be able to change them. 
    private void Start()
    {
        WinnerImage.GetComponent<Image>().sprite = GameManager.GetWinnerSprite();

        LooserImage.GetComponent<Image>().sprite = GameManager.GetLooserSprite();
        text.text = GameManager.GetWinnerName() + " WINS!!!";
    }

    public void RestartGame()
    {
        GameManager.Replay();
    }
    
    public void ChangeCharacters(){
        GameManager.ChangeCharacters();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
