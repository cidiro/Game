using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skills{
    public class SpearThrow : Attack
    {
        // Start is called before the first frame update
        [SerializeField]private GameObject proyectilePrefab;
        [SerializeField]private int proyectileSpeed=10;
        [SerializeField]private int verticalPower=2;
        private SpriteRenderer playerSprite;
        new void Start()
        {
            base.Start();
            playerSprite = player.GetComponent<SpriteRenderer>();
        }

        public override void UseSkill()
        {
            if(Time.time - lastUse >= cooldown){
                lastUse = Time.time;

                GameObject proyectile=Instantiate(proyectilePrefab,new Vector3(transform.parent.position.x, transform.parent.position.y,0),Quaternion.Euler(0,0,-90));
                proyectile.GetComponent<SpikeBallLife>().setDamage(attackDamage);
                proyectile.GetComponent<SpikeBallLife>().setThrowerId(player.GetComponent<Player>().getID());
                Rigidbody2D rbProy=proyectile.GetComponent<Rigidbody2D>();
                if(playerSprite.flipX == true){
                    rbProy.velocity = new Vector3(-proyectileSpeed , verticalPower,0);
                }else{
                    rbProy.velocity = new Vector3(proyectileSpeed , verticalPower,0);
                }
            }
        }

        public override float getCoolDown()
        {
            return (Time.time - lastUse) / cooldown;
        }
    }
}