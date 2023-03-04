using UnityEngine;

namespace Skills
{
    public class Swing : Attack
    {
        private bool swingState;
        private float localX, localY;
        
        private SpriteRenderer playerSprite;

        new private void Start()
        {
            base.Start();
            Debug.Log("Starting the Swing class...");
            
            playerSprite = player.GetComponent<SpriteRenderer>();
            
            localX = Mathf.Abs(transform.localPosition.x);
            localY = transform.localPosition.y;
        }

        private void Update()
        {
            if (playerSprite.flipX)
            {
                sprite.flipX = true;
                transform.position = new Vector3(transform.position.x - localX, transform.position.y + localY, 0f);
            }
            else
            {
                sprite.flipX = false;
                transform.position = new Vector3(transform.position.x + localX, transform.position.y + localY, 0f);
            }
        }

        public override void UseSkill()
        {
            if (Time.time >= nextUseTime)
            {
                Debug.Log("Ataque 2"); //Llega a entrar aqui, pero en si los ataques no hacen nada, asiq no se si es algo del animator, alguna variable o que
                if (swingState)
                    animator.SetTrigger("SwingA");
                else
                    animator.SetTrigger("SwingB");

                swingState = !swingState;

                Collider2D[] hitEnemies =
                    Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy != playerCollider)
                        enemy.GetComponent<Player>().TakeDamage(attackDamage);
                }
                
                nextUseTime = Time.time + 1f / cooldown;
            }
        }
    }
}
