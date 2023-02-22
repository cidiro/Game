using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Testing_Input_System: MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake() {
        rb=GetComponent<Rigidbody2D>();    
    }
    
    public void Jump(InputAction.CallbackContext context ){
        Debug.Log("Jump");
        if(rb.velocity.y<.1f && rb.velocity.y> -.1f){
             rb.velocity=new Vector2(rb.velocity.x, 14f);
        }
    }

    public void MoveLeft(){
        rb.velocity=new Vector2(-7, rb.velocity.y);             
    }

    public void MoveRight(){
        rb.velocity=new Vector2(7, rb.velocity.y);    
    }

}
