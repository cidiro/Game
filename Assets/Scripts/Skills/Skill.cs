using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected GameObject player; 
    [SerializeField] protected float cooldown = 2f;

    protected Animator animator;
    protected SpriteRenderer sprite;
    protected Collider2D playerCollider;
    protected float nextUseTime;

    protected void Start()
    {
        Debug.Log("Starting the Skill class...");
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        playerCollider = player.GetComponent<Collider2D>();
    }

    public abstract void UseSkill();
}
