using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownDisplayer : MonoBehaviour
{
    private GameObject player;
    private Skill basicAttack;
    private Skill specialAttack;
    [SerializeField] private int playerId;
    [SerializeField] private GameObject progressBarBA;
    [SerializeField] private float barWidthBA;
    [SerializeField] private GameObject progressBarSA;
    [SerializeField] private float barWidthSA;

    void Start()
    {
        if(playerId==1){
            player=GameManager.getPlayer1();
        }else{
            player=GameManager.getPlayer2();
        }
        basicAttack = player.GetComponent<PlayerCombat>().getBasicAttack();
        specialAttack = player.GetComponent<PlayerCombat>().getSpecialAttack();
    }

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
    }
}
