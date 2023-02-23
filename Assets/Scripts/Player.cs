using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerID;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private AudioSource deathSound;
    
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
        deathSound.Play();
        Destroy(textMesh.gameObject);
        Invoke("EndGame", 3f);
    }
    
    private void DisablePlayer()
    {
        gameObject.SetActive(false);
    }

    private void EndGame()
    {
        GameManager.Winner = (playerID == 1 ? 2 : 1);
        GameManager.EndGame();
    }

}
