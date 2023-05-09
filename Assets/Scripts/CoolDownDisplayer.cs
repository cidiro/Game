using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownDisplayer : MonoBehaviour
{
    //Player of who´s skills cooldown we want to monitor 
    private GameObject player;
    //The basic attack of the player
    private Skill basicAttack;
    //The special attack of the player
    private Skill specialAttack;
    //The dash of the player
    private Skill dash;

    //Id used to identify which of both players we want to monitor, if its 1 we´ll monitor player1, and of not we will monitor player2
    [SerializeField] private int playerId;

    [SerializeField] private GameObject progressBarBA; //Progress bar and bar width for the basic attack monitor
    [SerializeField] private float barWidthBA;

    [SerializeField] private GameObject progressBarSA; //Progress bar and bar width for the special attack monitor
    [SerializeField] private float barWidthSA;

    [SerializeField] private GameObject progressBarDash; //Progress bar and bar width for the dash monitor
    [SerializeField] private float barWidthDash; 

    void Start()
    {
        if(playerId==1){
            player=GameManager.getPlayer1();
        }else{
            player=GameManager.getPlayer2();
        }
        //After we set the player we get all of the things we want to monitor
        basicAttack = player.GetComponent<PlayerCombat>().getBasicAttack();
        specialAttack = player.GetComponent<PlayerCombat>().getSpecialAttack();
        dash = player.GetComponent<PlayerCombat>().getDash();
    }

    //In all 3 progress bars we want to, if the cooldown has went past the condition point, set the progress bar to its width, because
    //if not the bar will continue expanding, which is not desired. Thats why if the cooldown percentage/100 es equal orbiger than 1, 
    //we set the progress bar to its width.
    void Update()
    {
        if(basicAttack.getCoolDown()>=1){
            progressBarBA.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, barWidthBA);
        }else{
            progressBarBA.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, basicAttack.getCoolDown()*barWidthBA);
        }

        if(specialAttack.getCoolDown()>=1){
            progressBarSA.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, barWidthSA);
        }else{
            progressBarSA.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, specialAttack.getCoolDown()*barWidthSA);
        }
        
        if(dash.getCoolDown()>=1){
            progressBarDash.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, barWidthDash);
        }else{
            progressBarDash.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dash.getCoolDown()*barWidthDash);
        }
    }
}
