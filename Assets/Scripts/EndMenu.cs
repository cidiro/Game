using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private Image player1Image;
    [SerializeField] private Image player2Image;
    
    private void Start()
    {
        switch (GameManager.Winner)
        {
            case 1:
                player2Image.color = new Color(1, 1, 1, 0.2f);
                break;
            case 2:
                player1Image.color = new Color(1, 1, 1, 0.2f);
                break;
        }
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
