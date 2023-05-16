using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidKill : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.tag == "Player"){
            collision.gameObject.GetComponent<Player>().TakeDamage(1000);
        }
    }
}
