using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement: MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody2D rb;
    private PlayerInput input;
    [SerializeField]private float movementSpeed=5f;
    private InputsPlayer inputsPlayer;
    private enum MovementState { idle, running, jumping, falling }
    private SpriteRenderer sprite;
    private Animator anim;
    private Vector2 inputVec;
    private int idPlayer;
    [SerializeField] private AudioSource jumpSoundEffect;
    //private Collider2D coll;
    //[SerializeField] private LayerMask jumpableGround;

    private void Awake() {
        rb=GetComponent<Rigidbody2D>();
        input=GetComponent<PlayerInput>();
        //coll = GetComponent<BoxCollider2D>();
        sprite=GetComponent<SpriteRenderer>();  
        anim=GetComponent<Animator>();
    }

    public void EnableControlls(){
        inputsPlayer= new InputsPlayer();
        idPlayer=GetComponent<Player>().getID();
        if(idPlayer==1){
            Debug.Log(gameObject.name+"Subscrito a movement");
            inputsPlayer.Movement.Enable();
            inputsPlayer.Movement.Jump.performed += Jump;
        }else{
            Debug.Log(gameObject.name+"Subscrito a movement2");
            inputsPlayer.MovementP2.Enable();
            inputsPlayer.MovementP2.Jump.performed += Jump;
        }
    }

    private void Jump(InputAction.CallbackContext context){
        if(rb.velocity.y>-.1f && rb.velocity.y<.1f){
            //jumpSoundEffect.Play();
            rb.velocity=new Vector2(rb.velocity.x, 14f);
        }
       
    }

    private void Update() {
        if(idPlayer!=0){
            if(idPlayer==1){
                inputVec=inputsPlayer.Movement.Move.ReadValue<Vector2>();
            }else{
                inputVec=inputsPlayer.MovementP2.Move.ReadValue<Vector2>();
            }
            rb.velocity=new Vector2(inputVec.x*movementSpeed, rb.velocity.y);
            UpdateAnimationState(inputVec.x);
        }
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

    public float getInputX(){
        if(idPlayer!=0){
            if(idPlayer==1){
                return inputsPlayer.Movement.Move.ReadValue<Vector2>().x;
            }else{
                return inputsPlayer.MovementP2.Move.ReadValue<Vector2>().x;
            }
        }
        return 0f;
    }
    /*private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }*/
}