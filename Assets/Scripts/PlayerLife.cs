using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private TextMeshProUGUI textMesh;
    
    private int currentHealth;
    private Animator animator;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("hit");
        textMesh.text = currentHealth + "%";
        
        
        if (currentHealth <= 0)
            Death();
    }
    
    private void Death()
    {
        animator.SetBool("dead", true);
        rigidbody.bodyType = RigidbodyType2D.Static;
        Debug.Log(name + " died");
    }
    
    private void DisablePlayer()
    {
        gameObject.SetActive(false);
    }
    
}
