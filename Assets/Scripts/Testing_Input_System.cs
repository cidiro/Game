using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Testing_Input_System: MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput pi;

    private void Awake() {
        rb=GetComponent<Rigidbody2D>();    
        pi=GetComponent<PlayerInput>();
    }
    
    public void Jump(InputAction.CallbackContext context ){
        Debug.Log("Jump");
        if(rb.velocity.y<.1f && rb.velocity.y> -.1f){
             rb.velocity=new Vector2(rb.velocity.x, 14f);
        }
    }

    public void MoveLeft(InputAction.CallbackContext context){
        if(context.performed){
            rb.velocity=new Vector2(-7, rb.velocity.y);           
        }
    }

    public void MoveRight(InputAction.CallbackContext context){
        rb.velocity=new Vector2(7, rb.velocity.y);    
    }

    public void StopMoving(InputAction.CallbackContext context){
        if(context.canceled){
            rb.velocity=new Vector2(0, rb.velocity.y);
        }
    }
}
