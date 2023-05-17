using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesDamage : MonoBehaviour
{
    [SerializeField]private int damage=10;
    
    //if a player collides with the spikes, deal damage to him
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player"){
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
