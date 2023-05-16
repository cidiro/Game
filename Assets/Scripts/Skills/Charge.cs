using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skills{
    public class Charge : Attack
    {
        private SpriteRenderer playerSprite; 
        private Rigidbody2D rb;
        //this boolean will indicate if we are charging or not
        private bool active=false;
        private Vector2 colliderBounds;
        new void Start()
        {
            base.Start();
            playerSprite = player.GetComponent<SpriteRenderer>();
            rb=player.GetComponent<Rigidbody2D>();
            colliderBounds = player.GetComponent<BoxCollider2D>().bounds.size;
        }

        private void Update() {

            if (playerSprite.flipX)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
            if(active){
                //Check if something collides with the player, if thats the case do damage and set active to false, this because 
                //we know that there will only be one enemy, so 1 hit at max mecause we dont want to hit the same player more than
                //once in the same charge. If we dont set it to false it would instakill the enemy if hit.
                Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(transform.parent.position, colliderBounds, enemyLayers);
                foreach (Collider2D enemy in hitEnemies){
                    if(enemy != playerCollider){
                        if(enemy.GetComponent<Player>() != null){
                            enemy.GetComponent<Player>().TakeDamage(attackDamage);
                            active = false;
                        }
                    }
                }
            }    
        }

        public override void UseSkill()
        {
            if(Time.time - lastUse >= cooldown){
                lastUse = Time.time;
                active=true;
                player.GetComponent<PlayerMovement>().toggleDashing();
                if(playerSprite.flipX == true){
                    rb.velocity = new Vector2(-40f, rb.velocity.y);
                }else{
                    rb.velocity = new Vector2(40f, rb.velocity.y);//quizas poner la y como 0 para que el dash sea full horizontal siempre
                }
                animator.SetBool("charge",true);                
                StartCoroutine(chargeWait(0.5f));
            }
        }

        private IEnumerator chargeWait(float time){ //lo que hace esto es esperar el tiempo que le digamos y togglear el dash de vuelta a false para que se vuelva a 
            yield return new WaitForSeconds(time); //usar el update del PlayerMovement

            //We toggle the dashing back to false, since we have finished dashing.
            active=false;
            animator.SetBool("charge",false);
            player.GetComponent<PlayerMovement>().toggleDashing();
        }

        public override float getCoolDown()
        {
            return (Time.time - lastUse) / cooldown;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.parent.position, colliderBounds);
        }
    }
}