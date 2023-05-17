using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skills
{
    public class SpinningAttack : Attack
    {
        [SerializeField]private AudioSource attackSound;
        new private void Start()
        {
            base.Start();

            Debug.Log("Starting the Lick class...");
            
            localX = Mathf.Abs(transform.localPosition.x);
            localY = transform.localPosition.y;
            Debug.Log(localX + "" + localY);
        }

        private void Update()
        {
            attackPoint.transform.position = new Vector3(transform.parent.position.x - localX, transform.parent.position.y + localY, 0f);
        }

        public override void UseSkill(){

            //Check if enough time has past since the las time we used this attack
            if (Time.time-lastUse >= cooldown)
            {
                lastUse=Time.time;
                attackSound.Play();

                //In hitEnemies, we will store the Colliders of the enemies that are hit with our Box. For this, the position of the player sets
                //the center of the box, the vector made by the attackRange and 1 will define the half of the width and height of the box, and lastly 
                //the enemyLayers indicates in which layer the box will check for hits.
                Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.transform.position, new Vector2(attackRange, .75f), 0, enemyLayers);
                animator.SetTrigger("attack");                
                //Now we go through all the Colliders, and if a Collider belongs to a player, that player will take the damage corresponding to the attack.
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log(enemy.name);
                    if (enemy != playerCollider){
                        //swingHit.Play(); Descomentar cuando el ataque tenga un sonido de hit.
                        if(enemy.GetComponent<Player>()!=null){//Porque aqui ns porque me intenta usar el metodo con las paredes, y da error pq no lo tienen
                            enemy.GetComponent<Player>().TakeDamage(attackDamage);
                        }
                       
                        
                    }
                }
            }
        }

        public override float getCoolDown(){
            return (Time.time-lastUse) / cooldown;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackPoint.transform.position, new Vector3(attackRange, 1, 0));
        }
    }

}
