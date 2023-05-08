using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public void addCharacter(GameObject character){
        GameManager.addPlayer(character);
        
    }
}
