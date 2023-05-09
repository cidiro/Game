using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skills
{
    public class SpinningAttack : Attack
    {
        private float lastUse=0;
        private void Start()
        {
            base.Start();
        }

        private void Update()
        {

        }

        public override void UseSkill(){

            //Debug.Log(LayerMask.LayerToName(enemyLayers)); //btw, asi se ve el nombre de una layer


            //Check if enough time has past since the las time we used this attack
            if (Time.time-lastUse >= cooldown)
            {
                lastUse=Time.time;
                //swingDone.Play(); Descomentar cuando el ataque tenga un sonido.
                Debug.Log("Spin");

                //In hitEnemies, we will store the Colliders of the eemies that are hit with our Box. For this, the position of the player sets
                //the center of the box, the vector made by the attackRange and 1 will define the half of the width and height of the box, and lastly 
                //the enemyLayers indicates in which layer the box will check for hits.
                Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(transform.parent.position, new Vector2(attackRange, 1), enemyLayers);
                
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
    }

}
