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
    //The name of the sceene which has the world the players are going to fight in
    private static string worldName;

    //To start a game we load the next sceene and place the charatcers in their "spawns", thats what the transforms are for.
    public static void StartGame()
    {
        SceneManager.LoadScene(worldName);

        if(worldName == "SampleScene"){ //Las coordenadas de spawn en el mundo 1
            player1.transform.position = new Vector3(-3.33f, 1, 0);
            player2.transform.position = new Vector3(13.33f, 1, 0);
        }else{//Las coordenadas de spawn en el mundo 2
            player1.transform.position = new Vector3(-5f, -1, 0);
            player2.transform.position = new Vector3(17f, -1, 0);
        }
        //Al entrar a partida descongelamos los jugadores para que se puedan mover
        player1.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePosition;
        player1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player2.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePosition;
        player2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public static void addPlayers(GameObject p1, GameObject p2){
        player1=p1;
        player2=p2;
        
    }

    public static void dissablePlayers(){
        //player1.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        //player2.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        
        player1.GetComponent<Player>().disableControls();
        player2.GetComponent<Player>().disableControls();
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
            player1.transform.position=new Vector3(-3.33f, 1, 0);
            player1.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezePosition;
            DontDestroyOnLoad(player1);
            copyp1=character;
            return;
        }
        if(character.name!=player1.name){
            player2=Instantiate(character);
            player2.name=character.name;
            player2.GetComponent<Player>().setID(2);
            player2.GetComponent<PlayerMovement>().EnableControlls();
            player2.transform.position=new Vector3(13.33f, 1, 0);  
            player2.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezePosition;
            DontDestroyOnLoad(player2);
            copyp2=character;
            //StartGame();
            NextScene();
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

        StartGame();
    }
    
    //Method used when the player selects to change characters at the end of a fight, this means, going back to the character selection scene,
    //choosing the characters, choosing the world/map, and the fighting again. 
    public static void ChangeCharacters(){
        //We first delete the recent characters, because we want to select new ones
        player1.GetComponent<Player>().disableControls();
        Destroy(player1);
        player2.GetComponent<Player>().disableControls();
        Destroy(player2);
        //Destroy(copyp1);
        //Destroy(copyp2);

        //Go to the character selection scene
        SceneManager.LoadScene("CharacterSelection");
    }
    
    public static void EndGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("End Screen");
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

    public static string GetWinnerName(){
        if(Winner == 1){
            return player1.name;
        }
        return player2.name;

    }

    //Params: worldName-> The name of the scene in which the characters will fight
    //Method called when selecting a world in the World Selection scene, in this we use the world name we recieve 
    //to set the world name of the game manager, and then, because thats the only thing we have to do in the world
    //selection scene, we start the game (go to the selected world so players can fight)
    public static void SetWorld(string worldName_){
        worldName = worldName_; 
        StartGame();
    }

    //Params: sceneName-> The name of the scene we are switching to
    //Method used when wanting to switch to a specific scene
    public static void GoToScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    //Method used when wanting to switch to the next scene, acording to the indexes of the scenes in the build settings.
    public static void NextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static GameObject getPlayer1(){
        return player1;
    }

    public static GameObject getPlayer2(){
        return player2;
    }

}
