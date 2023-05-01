using UnityEngine;
using System.Collections;
namespace Skills
{
    public class Dash : Utility /*****Se me olvido ponerlo en el comment del commit, pero el dash esta en el left shift y en el boton oeste (X en xbox, 
                                                                                                                                     cuadrado en play)*****/
    {       
        private SpriteRenderer playerSprite; 
        private Rigidbody2D rb;
        private float lastUse=0;

        //Ambos playerSprite y rb son referencias al SpriteRenderes y RigidBody2d del player, asique en el start las seteamos.
        new private void Start(){
            playerSprite = player.GetComponent<SpriteRenderer>();
            rb=player.GetComponent<Rigidbody2D>();
        }


        public override void UseSkill()
        {
            //Check that the cooldown time has past since the las time we used the dash.
            if(Time.time-lastUse>=cooldown){
                //Update the last time because now this is the new las time we used the dash.
                lastUse=Time.time;
                //We toggle the dashing variable to true while the dash ocurs.
                player.GetComponent<PlayerMovement>().toggleDashing();
                //If the player is looking to the left (sprite is flipped) the X value of the dash will be negative, if he is looking to the right
                //the X value will be positive.
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

            //We toggle the dashing back to false, since we have finished dashing.
            player.GetComponent<PlayerMovement>().toggleDashing();
        }
    }
}


