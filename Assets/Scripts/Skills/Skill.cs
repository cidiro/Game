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
}
