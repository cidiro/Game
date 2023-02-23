using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackRate = 2f;
    
    [SerializeField] private Animator animator;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    
    private Collider2D playerCollider;

    private bool swing;
    private float nextAttackTime;
    
    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    
    private void Attack()
    {

        if (swing)
            animator.SetTrigger("SwingA");
        else
            animator.SetTrigger("SwingB");
        
        swing = !swing;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy != playerCollider)
                enemy.GetComponent<Player>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
