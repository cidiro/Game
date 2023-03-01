using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class AttackP1 : MonoBehaviour
{
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackRate = 2f;
    
    [SerializeField] private Animator animator;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private AudioSource impactSound;
    
    private Collider2D playerCollider;
    private InputsPlayer playerInputs;

    private bool swing;
    private float nextAttackTime;
    
    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        playerInputs=new InputsPlayer();
        playerInputs.Movement.Attack.Enable();
        playerInputs.Movement.Attack.performed += Attack;
    }    

    private void Attack(InputAction.CallbackContext context)
    {
        if(Time.time >= nextAttackTime){
            if (swing)
                animator.SetTrigger("SwingA");
            else
                animator.SetTrigger("SwingB");
            
            attackSound.Play();
            swing = !swing;

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy != playerCollider){
                    enemy.GetComponent<Player>().TakeDamage(attackDamage);
                    impactSound.Play();
                }
            }
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
