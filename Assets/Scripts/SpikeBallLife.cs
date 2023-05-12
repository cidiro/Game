using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallLife : MonoBehaviour
{
    private int damage;
    private int throwerID;

    private void Start() {
        StartCoroutine(Death(4));    
    }

    public void setDamage(int dmg){
        damage = dmg;
    }  

    public void setThrowerId(int id){
        throwerID = id;
    }
    private void Update() {
        Debug.Log(damage);
    }    
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Player>().getID() != throwerID){
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator Death(float lifeDuration){
        yield return new WaitForSeconds(lifeDuration);

        Destroy(this.gameObject);
    }
}
