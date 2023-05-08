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
        /*if(instance!=null){
            Destroy(this.gameObject);
            return;
        }
        instance=this;
        GameObject.DontDestroyOnLoad(this.gameObject);*/
    }
    public void addCharacter(GameObject character){
        GameManager.addPlayer(character);
        
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
