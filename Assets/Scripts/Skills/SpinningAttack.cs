using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skills
{
    public class SpinningAttack : Attack
    {
        private float lastUse=0;
        void Start()
        {
            base.Start();
        }

        void Update()
        {

        }

        public override void UseSkill(){
            if (Time.time-lastUse >= cooldown)
            {
                lastUse=Time.time;
                //swingDone.Play();
                Debug.Log("Spin");
                //Debug.Log(LayerMask.LayerToName(enemyLayers)); //btw, asi se ve el nombre de una layer
                Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(transform.parent.position, new Vector2(attackRange, 1), enemyLayers);
                Debug.Log(hitEnemies.Length);
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log(enemy.name);
                    if (enemy != playerCollider){
                        //swingHit.Play();
                        if(enemy.GetComponent<Player>()!=null){//Porque aqui ns porque me intenta usar el metodo con las paredes, y da error pq no lo tienen
                            enemy.GetComponent<Player>().TakeDamage(attackDamage);
                        }
                       
                        
                    }
                }
            }
        }
    }

}
