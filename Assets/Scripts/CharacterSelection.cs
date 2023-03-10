using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public float player1_x, player1_y, player2_x, player2_y;
    private bool player1Set=false;
    private static CharacterSelection instance;

    private void Start() {
        if(instance!=null){
            Destroy(this.gameObject);
            return;
        }
        instance=this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    public void addCharacter(GameObject character){
        if(!player1Set){
            player1=Instantiate(character);
            player1.name=character.name;
            player1.GetComponent<Player>().setID(1);
            player1.GetComponent<PlayerMovement>().EnableControlls();
            DontDestroyOnLoad(player1);
            player1Set=true;
            return;
        }
        if(character.name!=player1.name){
            player2=Instantiate(character);
            player2.name=character.name;
            player2.GetComponent<Player>().setID(2);
            player2.GetComponent<PlayerMovement>().EnableControlls();
            DontDestroyOnLoad(player2);
            GameManager.addPlayers(player1,player2);
            StartFight();
        }
        
    }
    
    private void StartFight(){
        GameManager.StartGame();
        player1.transform.position=new Vector3(player1_x, player1_y, 0);
        player2.transform.position=new Vector3(player2_x, player2_y, 0);
    }
    public GameObject getPlayer1(){
        return player1;
    }

    public GameObject getPlayer2(){
        return player2;
    }


}
