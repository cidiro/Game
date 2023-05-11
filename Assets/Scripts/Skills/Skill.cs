using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    //Player that uses the skill.
    [SerializeField] protected GameObject player; 

    //Cooldown in order to use the skill again
    [SerializeField] protected float cooldown = 2f;
    
    //Last time the Skill was used
    protected float lastUse=0;

    //Refferences to the animator, sprite and playerColider of the player that uses the skill.
    protected Animator animator;
    protected SpriteRenderer sprite;
    protected Collider2D playerCollider;
    
    protected void Start()
    {
        Debug.Log("Starting the Skill class...");
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        playerCollider = player.GetComponent<Collider2D>();
    }

    public abstract void UseSkill();

    //This method, in all its implementations, will return the percentage of the cooldown completed / 100, so, if
    //the full time of the cooldown has went by, it will return 1 or more, if we are half way through the cooldown 
    //it will return 0.5, and so on. 
    public abstract float getCoolDown();
}
