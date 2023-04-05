using UnityEngine;
using System.Collections;
namespace Skills
{
    public class Dash : Utility
    {       
        private SpriteRenderer playerSprite; 
        private Rigidbody2D rb;
        private float lastUse=0;
        new private void Start(){
            playerSprite = player.GetComponent<SpriteRenderer>();
            rb=player.GetComponent<Rigidbody2D>();
        }
        public override void UseSkill()
        {
            if(Time.time-lastUse>=cooldown){

                lastUse=Time.time;
                player.GetComponent<PlayerMovement>().toggleDashing();
                if(playerSprite.flipX == true){ 
                    rb.velocity = new Vector2(-30f, rb.velocity.y); //El -30 y abajo 30 son las velocidades de impulso, he preferido poner un dash corto (0.25s) y "rapido"
                                                                    //que uno mas lento y que dure mas, creo que asi queda mejor.
                }else{
                    rb.velocity = new Vector2(30f, rb.velocity.y);  //El +-30 y el 0.25 podrian hacerese variables serializadas para cambiarlas desde unity, pero por
                                                                    //ahora creo que se pueden dejar asi pq tmpoco vamos a jugar demasiado con la velocidad y tiempo 
                                                                    //del dash, una vez se encuentre los adecuados se quedaran fijos, entonces por eso digo q sobra 
                                                                    //ponerlo como una variable serialized, mejor dejarlo asi
                }
                StartCoroutine(dashWait(0.25f)); //El tiempo que durara el dash
            }
            //throw new System.NotImplementedException();
        }

        private IEnumerator dashWait(float time){ //lo que hace esto es esperar el tiempo que le digamos y togglear el dash de vuelta a false para que se vuelva a 
            yield return new WaitForSeconds(time); //usar el update del PlayerMovement
            player.GetComponent<PlayerMovement>().toggleDashing();
        }
    }
}


