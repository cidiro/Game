using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField]private GameObject player1;
    [SerializeField]private GameObject player2;
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
            DontDestroyOnLoad(player1);
            player1Set=true;
            return;
        }
        if(character.name!=player1.name){
            player2=Instantiate(character);
            player2.name=character.name;
            DontDestroyOnLoad(player2);
            GameManager.StartGame();
        }
        
    }
    public GameObject getPlayer1(){
        return player1;
    }

    public GameObject getPlayer2(){
        return player2;
    }


}
