using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //This int will, at the end of each fight, have the id of the winner.
    public static int Winner;

    //The GameManager has both the players (characters in fight) and a copy of each, this copy is needed because
    //when doing a replay, is easier to delete the player and create a new one from the copy than restoring all
    //the values of the existing player. This because when a player dies not only the hp stat is changed, theres also
    //values related to the collider and RigidBody that are modified.
    private static GameObject player1=null, player2=null, copyp1, copyp2;
    
    //To start a game we load the next sceene and place the charatcers in their "spawns", thats what the transforms are for.
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

    //Params: character-> The player/character to be added to the fight.
    //When adding a player individualy, because the order of selection is always player1 before player2, we check if the player1 
    //exists, if it doesnt we save the character as player1, if player1 exists we save the character as player2 and then, because
    //this means we have both our fighters, we start the game.
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

    //When replaying a fight, we want to first "re-add" the characters. We first dissable and delete the existing players
    //and then use their copies to create new ones.
    public static void Replay(){
        //Dissabling and deleting the players
        player1.GetComponent<Player>().disableControls();
        Destroy(player1);
        player2.GetComponent<Player>().disableControls();
        Destroy(player2);
        
        //Creating the new players from the copies
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

        //Going back to the fight sceene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
        player1.transform.position=new Vector3(-3.33f, 1, 0);
        player2.transform.position=new Vector3(13.33f, 1, 0);       

    }
    
    public static void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Method that returns the sprite of the winner, based on the value of Winner (int) 
    public static Sprite GetWinnerSprite(){
        if(Winner == 1){
            return player1.GetComponent<SpriteRenderer>().sprite; //This returns the most recent sprite of the player, so its kind of random, if we want the 
                                                                  //winner/looser sprites to be something specific, like for example them mid jump, ill change this
                                                                  //to get the sprite from the animator, but i think with this is ok.
        }
        return player2.GetComponent<SpriteRenderer>().sprite;
    }

    //Method that returns the sprite of the looser, based on the value of Winner (int)
    public static Sprite GetLooserSprite(){
        if(Winner == 1){
            //If the winner is 1 and we want to return the looser, we return 2.
            return player2.GetComponent<SpriteRenderer>().sprite;
        }
        return player1.GetComponent<SpriteRenderer>().sprite;

    }

    public static GameObject getPlayer1(){
        return player1;
    }

    public static GameObject getPlayer2(){
        return player2;
    }
}
