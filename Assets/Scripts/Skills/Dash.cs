using UnityEngine;
namespace Skills
{
    public class Dash : Utility
    {       
        private SpriteRenderer playerSprite; 
        private Rigidbody2D rb;
        new private void Start(){
            playerSprite = player.GetComponent<SpriteRenderer>();
            rb=player.GetComponent<Rigidbody2D>();
        }
        public override void UseSkill()
        {
            if(playerSprite.flipX == true){ //deja modificar la y pero la x no, no se porque
                rb.velocity = new Vector2(-20f, rb.velocity.y);
                Debug.Log("a");
            }else{
                rb.velocity = new Vector2(20f, rb.velocity.y);
                Debug.Log("b");
            }
            //throw new System.NotImplementedException();
        }
    }
}


