using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_InputOn : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody2D rb;
    private PlayerInput input;
    [SerializeField]private float movementSpeed=5f;
    private InputsPlayer inputsPlayer;
    private enum MovementState { idle, running, jumping, falling }
    private SpriteRenderer sprite;
    private Animator anim;
    [SerializeField] private AudioSource jumpSoundEffect;
    //private Collider2D coll;
    //[SerializeField] private LayerMask jumpableGround;

    private void Awake() {
        rb=GetComponent<Rigidbody2D>();
        input=GetComponent<PlayerInput>();
        //coll = GetComponent<BoxCollider2D>();
        sprite=GetComponent<SpriteRenderer>();  
        anim=GetComponent<Animator>();

        inputsPlayer= new InputsPlayer();
        inputsPlayer.Movement.Enable();

        inputsPlayer.Movement.Jump.performed += Jump;
    }

    private void Jump(InputAction.CallbackContext context){
        if(rb.velocity.y>-.1f && rb.velocity.y<.1f){
            //jumpSoundEffect.Play();
            rb.velocity=new Vector2(rb.velocity.x, 14f);
        }
       
    }

    private void Update() {
        Vector2 inputVec=inputsPlayer.Movement.Move.ReadValue<Vector2>();
        rb.velocity=new Vector2(inputVec.x*movementSpeed, rb.velocity.y);
        UpdateAnimationState(inputVec.x);
    }


    private void UpdateAnimationState(float dirX)
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    /*private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }*/
}
