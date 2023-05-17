using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skills{
    public class Lick : Attack
    {
        private SpriteRenderer playerSprite;
        [SerializeField] private float slowDuration = 4;
        [SerializeField]private AudioSource lickDone;
        new void Start()
        {
            base.Start();
            Debug.Log("Starting the Lick class...");
            
            playerSprite = player.GetComponent<SpriteRenderer>();
            localX = Mathf.Abs(transform.localPosition.x);
            localY = transform.localPosition.y;
            Debug.Log(localX + "" + localY);
        }

        private void Update() {
            if (playerSprite.flipX)
            {
                sprite.flipX = true;
                attackPoint.transform.position = new Vector3(transform.parent.position.x - localX, transform.parent.position.y + localY, 0f);
            }
            else
            {
                sprite.flipX = false;
                attackPoint.transform.position = new Vector3(transform.parent.position.x + localX, transform.parent.position.y + localY, 0f);
            }
        }

        public override void UseSkill()
        {
            if(Time.time - lastUse >= cooldown){
                lastUse=Time.time;
                animator.SetTrigger("lick");
                lickDone.Play();
                Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.transform.position, new Vector2(attackRange,1), 0, enemyLayers);
                foreach(Collider2D enemy in hitEnemies){
                    if(enemy!=playerCollider && enemy.GetComponent<Player>()!=null){
                        enemy.GetComponent<Player>().TakeDamage(attackDamage);
                        enemy.GetComponent<PlayerMovement>().Slow(slowDuration);
                    }
                }
            }
        }

        public override float getCoolDown()
        {
            return (Time.time - lastUse) / cooldown;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackPoint.transform.position, new Vector3(attackRange,1,0));
        }
    }
}