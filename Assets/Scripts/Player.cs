using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerID;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private AudioSource deathSound;
    
    private int currentHealth;
    private Animator animator;
    private new Rigidbody2D rigidbody;

    private void Start()
    {
        currentHealth = maxHealth;

        //Both animator and rigidbody refference the Animator and RigidBody2D of the player, so when wanting to use one of both
        //we can use them instead of having to do GetComponent<... each time.
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    
    //Params: damage-> The ammoun of health that has to be deducted from the player HP
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("hit");
        
        if (currentHealth <= 0)
            Death();
    }
    
    private void Death()
    {
        Debug.Log(gameObject.name+ "dies");
        
        //We set dead to true in the animator to trigger the death animation, and also make the body static, so it cant move no more.
        animator.SetBool("dead", true);
        rigidbody.bodyType = RigidbodyType2D.Static;
        GameManager.dissablePlayers();
        deathSound.Play();

        //Call the EndGame method in this class after waiting for 3 seconds.
        Invoke("EndGame", 3f);
    }
    
    private void DisablePlayer()
    {
        gameObject.SetActive(false);
    }

    //Used when deleting a character. We want to dissable both the combat and movement controlls, because if we dont dissable them and then delete the player
    //it´ll lead to bugs or crashing.
    public void disableControls(){
        GetComponent<PlayerMovement>().disableControls();
        GetComponent<PlayerCombat>().disableControls();
    }

    private void EndGame()
    {
        //Because this will happen of the player that lost, when setting who is the winner we want to set to the opposite of this player. For this we
        //check if the id of this player is 1, if it is we set winner to 2, and if his id isnt 1, we set the winner to 1. In both cases we are setting to the oposite.
        GameManager.Winner = (playerID == 1 ? 2 : 1);
        GameManager.EndGame();
    }
    
    public int getID(){
        return playerID;
    }
    
    public void setID(int id){
        playerID=id;
    }

    public void RecoverHP(){
        currentHealth = maxHealth; 
    }

    public int GetCurrentHealth(){
        return currentHealth;
    }

    public int GetMaxHealth(){
        return maxHealth;
    }

}
