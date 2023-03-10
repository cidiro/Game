using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int Winner;
    private static GameObject player1, player2;
    
    public static void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void addPlayers(GameObject p1, GameObject p2){
        player1=p1;
        player2=p2;
    }
    public static void Replay(){
        Debug.Log("a");
        player1.transform.position=new Vector3(-3, 1, 0);
        player1.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Dynamic;  //Ver si mejor crear una copia de 0 en vez de reiniciarles todo
        player1.GetComponent<Player>().RecoverHP();
        player2.transform.position=new Vector3(11, 1, 0);
        player2.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Dynamic;
        player2.GetComponent<Player>().RecoverHP();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
    
    public static void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
