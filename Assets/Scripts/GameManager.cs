using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int Winner;
    private static GameObject player1=null, player2=null, copyp1, copyp2;
    
    public static void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        player1.transform.position=new Vector3(-3.33f, 1, 0);
        player2.transform.position=new Vector3(13.33f, 1, 0);
    }

    public static void addPlayers(GameObject p1, GameObject p2){
        player1=p1;
        player2=p2;
        
    }
    public static void addPlayer(GameObject character){
        if(player1==null){
            player1=Instantiate(character);
            player1.name=character.name;
            player1.GetComponent<Player>().setID(1);
            player1.GetComponent<PlayerMovement>().EnableControlls();
            DontDestroyOnLoad(player1);
            copyp1=character;
            return;
        }
        if(character.name!=player1.name){
            player2=Instantiate(character);
            player2.name=character.name;
            player2.GetComponent<Player>().setID(2);
            player2.GetComponent<PlayerMovement>().EnableControlls();
            DontDestroyOnLoad(player2);
            copyp2=character;
            StartGame();
        }
    }
    public static void Replay(){
        player1.GetComponent<Player>().disableControls();
        Destroy(player1);
        player2.GetComponent<Player>().disableControls();
        Destroy(player2);
        
        player1=Instantiate(copyp1);
        player1.name=copyp1.name;
        player1.GetComponent<Player>().setID(1);
        player1.GetComponent<PlayerMovement>().EnableControlls();
        DontDestroyOnLoad(player1);        

        player2=Instantiate(copyp2);
        player2=Instantiate(copyp2);
        player2.name=copyp2.name;
        player2.GetComponent<Player>().setID(2);
        player2.GetComponent<PlayerMovement>().EnableControlls();
        DontDestroyOnLoad(player2);        

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
        player1.transform.position=new Vector3(-3.33f, 1, 0);
        player2.transform.position=new Vector3(13.33f, 1, 0);       

    }
    
    public static void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
