using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpDisplayer : MonoBehaviour
{
    [SerializeField] private int playerId;
    [SerializeField] private GameObject imageDisplay;
    [SerializeField] private GameObject textDisplay;
    private GameObject player;
    //[SerializeField] private TextMeshProUGUI a; aqui poner el texto donde va la HP, asi lo podemos gestionar aqui, desde el update, o quizas no, 
    //puede que el player al recibir daño avise al display para actualizarlo al momento, que con el update hay un rate suficiente, pero de esta forma
    //sería 100% al instante;

    //Tener el player siempre aqui y ir cogiendole la vida o pedir en cada update la vida al gameManager, que es mas optimo?
    private void Start() {
        if(playerId==1){
            player=GameManager.getPlayer1();
        }else{
            player=GameManager.getPlayer2();
        }
        imageDisplay.GetComponent<Image>().sprite=player.GetComponent<SpriteRenderer>().sprite;
    }


    void Update()
    {
        int hp=player.GetComponent<Player>().GetCurrentHealth();
        if(hp <= 0){
            textDisplay.GetComponent<TextMeshProUGUI>().text = "0";
        }else{
            textDisplay.GetComponent<TextMeshProUGUI>().text = hp + "";
        }
        
    }
}
