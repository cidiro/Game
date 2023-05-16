using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement: MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody2D rb;
    private PlayerInput input;
    [SerializeField]private float movementSpeed=5f;
    [SerializeField]private float jumpForce=14f;
    private InputsPlayer inputsPlayer;
    private enum MovementState { idle, running, jumping, falling }
    private SpriteRenderer sprite;
    private Animator anim;
    private Vector2 inputVec;
    private int idPlayer;
    private bool dashing = false;
    private InputDeviceController deviceController;

    //private Collider2D coll;
    //[SerializeField] private LayerMask jumpableGround;


    //All four of the variables we set inside the awake are references to components of the player so we dont have to use GetComponent... each time
    //we want to access the components.
    private void Awake() {
        rb=GetComponent<Rigidbody2D>();
        input=GetComponent<PlayerInput>();
        //coll = GetComponent<BoxCollider2D>();
        sprite=GetComponent<SpriteRenderer>();  
        anim=GetComponent<Animator>();
        deviceController=GameObject.Find("InputDeviceManager").GetComponent<InputDeviceController>();
        idPlayer=GetComponent<Player>().getID();
    }

    //When enabling the controls of a player, we want to first enable the desired Action Maps, in this case, if we are the player1 we want to enable the
    //Movement action map, and if we are player2 we want to enable the MovementP2 action map. After that we want to subscribe to the jump input, so when
    //the input associated to jumping for a player is pressed it will call the jump method.
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

    //For disabling the controls we firs disable the action map that the player is using and the unsubscribe to the jump method.
    public void disableControls(){
        idPlayer=GetComponent<Player>().getID();
        if(idPlayer==1){
            inputsPlayer.Movement.Disable();
            inputsPlayer.Movement.Jump.performed -= Jump;
            //inputsPlayer.Movement.Move.performed -= Move;
        }else{
            inputsPlayer.MovementP2.Disable();
            inputsPlayer.MovementP2.Jump.performed -= Jump;
            //inputsPlayer.MovementP2.Move.performed -= Move;
        }
    }

    //Params: context-> In this case the context is needed for the Jump method to be able to subscribe to the Jump input. Its not something you can choose or
    //not to do, its required by unity if you want to subscribe to an input.
    //When jumping we want to check if the Y velocity (vertical) of the player is 0 (in this case we ose -.1f and .1f because normaly, because of being a vector
    //the velovity is never 0, so the closes thing to ==0 is to use >-.1f and <.1f), if th velocity is 0 the player isnt in the air so we can jump,
    //if its not 0 it meand he is either jumping or falling, and in neither case we can perform a jump.
    private void Jump(InputAction.CallbackContext context){
        Debug.Log(gameObject.name + " jumped");
        if(context.control.device.deviceId == deviceController.getPlayerControllerId(idPlayer) || context.control.device is Keyboard){
            if(rb.velocity.y>-.1f && rb.velocity.y<.1f){
                rb.velocity=new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    private void Move(InputAction.CallbackContext context){
        Debug.Log(this.gameObject.name + "move");
        if(context.control.device.deviceId == deviceController.getPlayerControllerId(idPlayer) || context.control.device is Keyboard){
            if(idPlayer == 1){
                inputVec=inputsPlayer.Movement.Move.ReadValue<Vector2>();
            }else{
                inputVec=inputsPlayer.MovementP2.Move.ReadValue<Vector2>();
            }
        } 
    }


    private void Update() {
        inputVec=new Vector2(0,0);
        if(idPlayer!=0 && !dashing){
            InputDevice leftJoyStick = deviceController.getDeviceOfPlayer(idPlayer); //Miramos si hay input del joystick del mando del jugador, si no tiene mando será null
            if(leftJoyStick != null){//Si si que tiene mando leemos el valor del joystick iszuierdo para el movimiento
                inputVec = leftJoyStick.GetChildControl<Vector2Control>("leftStick").ReadValue();
            }
            if(inputVec.x < .4 && inputVec.x > -.4){//aqui, comprobamos la x ya que, si no hay mando será 0, y si hay mando pero esta por debajo de 0.4 y encima de -0.4 podria deberse a que haya
                                                    //drifting en el joystick en vez de que el jugador realmente quiera moverse, asique en cualquiera de los dos casos leemos Move, que esta asociado al
                                                    //movimiento en teclado
                if(idPlayer == 1){
                    inputVec =inputsPlayer.Movement.Move.ReadValue<Vector2>();
                }else{
                    inputVec =inputsPlayer.MovementP2.Move.ReadValue<Vector2>();
                }
            }
            //En general lo que hemos hecho hasta llegar aqui es: si el jugador tiene mando y la x no esta en el rango que podria ser movimiento no intencional, usamos los datos de el mando para el 
            //movimiento, pero si, o el jugador no tiene mando o los valores del mando estan en el rango que podrian ser no intencionales, se usan los datos del teclado/move.
            rb.velocity=new Vector2(inputVec.x*movementSpeed, rb.velocity.y);
            UpdateAnimationState(inputVec.x);
        }
    }

    //Params: dirX-> A float that comes from the movement vector of a player, its allways between -1(full left) and 1(full right), we use this nuber to know 
    //the player is moving (=!0), and in that case, if he is going left (<0) or right (>0).
    //This method is used to update the state of the player, which indicates if he is running, jumping, etc.. and its later used in the animator to show the 
    //corresponding animation.
    private void UpdateAnimationState(float dirX)
    {
        MovementState state;

        if (dirX > 0f) //Moving right
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f) //Moving left
        {
            state = MovementState.running;
            sprite.flipX = true; //As the sprites are dome looking to the right, if we are moving towards the left we want to flip them.
        }
        else //Not moving
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f) //Jumping
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f) //Falling
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state); //Update the state value in the animator.
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

    //Method used to toggle the value of the dashing variable between true and false, this indicates, as the name says, if the 
    //player is currently dashing or not
    public void toggleDashing(){
        dashing = (dashing==false); //This toggles the value of dashing, we could also use a if(dashin==true){dashing=false}... but this is more simple.
    }

    public void Slow(float time){
        movementSpeed-=2;
        StartCoroutine(slowRecoverWait(time));
    }

    private IEnumerator slowRecoverWait(float time){ 
        yield return new WaitForSeconds(time);
        movementSpeed+=2;
    }

    /*private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }*/
}