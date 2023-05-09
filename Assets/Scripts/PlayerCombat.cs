using System;
using System.Collections;
using System.Collections.Generic;
using Skills;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Skill skill1;
    [SerializeField] private Skill skill2;
    [SerializeField] private Skill skill3;
    [SerializeField] private Skill dash;
    private InputsPlayer input;

    //As soon as a player is created to want to set his combat inputs, this is why we do it in the Start method.
    private void Start()
    {
        /*De Guille: Te lo dejo como nota aunque ya te dije, pero si quieres me encargo yo de "traducir" las cosas para que usen el input system.
        Pero de todas formas, para cuando sigas este script echa un ojo a player movement o en general como gestionar el tema de usar un input para cada jugador,
        porque atacaran con teclas distintas, y eso si se puede administrar desde un solo script (este) en vez de tener que usar un script para cada player mejor. 
        En general, que si lo puedes hacer lo mas generalizado posible (osea a poder ser que los jugadores compartan script), mejor*/
        

      
        input = new InputsPlayer();

        //There are movements assigned for the player1 (k&m) and player2 (controller), so we check who this player is, if it´s player1 we 
        //enable the combat related inputs in the Movement action map, if he was the player2 we would enable the inputs in the 
        //MovementP2 action map, as those are the ones asigned for the player2 (controller player).
        if(GetComponent<Player>().getID() == 1){
            Debug.Log("Suscrito a los ataquesP1"+gameObject.name);
            
            //First enable all the inputs we want to use.
            input.Movement.Attack.Enable();
            input.Movement.Attack2.Enable(); //Activas los ataques de el player1
            input.Movement.Attack3.Enable();
            input.Movement.Dash.Enable();

            //Then subscribe to that inputs, by subscribing to and input, what happens is that, in this case, when the input is performed
            //it will call the method we associated to it. For example in this case, when the Attach3 input of the player1 is used, it will
            //call the useSkill3 method.
            input.Movement.Attack.performed += useSkill1;
            input.Movement.Attack2.performed += useSkill2; //Subscribes las funciones use skill a cada input, por lo que si se pulsa la tecla vinculada a Attack
            input.Movement.Attack3.performed += useSkill3; //se llamara a useSkill1, Attack2 llamará a useSkill2, etc...
            input.Movement.Dash.performed += useDash;
        }else{
            Debug.Log("Suscrito a los ataquesp2"+gameObject.name);
            input.MovementP2.Attack.Enable();
            input.MovementP2.Attack2.Enable();
            input.MovementP2.Attack3.Enable();
            input.MovementP2.Dash.Enable();

            input.MovementP2.Attack.performed += useSkill1;
            input.MovementP2.Attack2.performed += useSkill2;
            input.MovementP2.Attack3.performed += useSkill3;
            input.MovementP2.Dash.performed += useDash;
        }

    }

    //This are the useSkill methods we see above, when subscribing to an input this are needed, they dont need to be allways called useSkill,
    //but they are needed because we need the CallbackContext for it to work, even if we dont use it. This methods call the actual skills and 
    //dash of the player.
    private void useSkill1(InputAction.CallbackContext context){ //El callback context solo esta porque hace falta para poder subscribir la funcion, pero no
        skill1.UseSkill();                                       //hacemos nada con el a parte de declararlo como entrada
    }
    private void useSkill2(InputAction.CallbackContext context){
        skill2.UseSkill();
    }
    private void useSkill3(InputAction.CallbackContext context){
        skill3.UseSkill();
    }
    private void useDash(InputAction.CallbackContext context){
        dash.UseSkill();
    }

    //For disabling the combat controls of a player, we want to first disable the inputs we earlier activated, and then we unsubscribe to the imputs
    //this to avoid the inputs later on trying to call to methods that not longer exist, which leads to crashing.
    public void disableControls(){
        if(GetComponent<Player>().getID() == 1){
            input.Movement.Attack.Disable();
            input.Movement.Attack2.Disable(); 
            input.Movement.Attack3.Disable();
            input.Movement.Dash.Disable();

            input.Movement.Attack.performed -= useSkill1;
            input.Movement.Attack2.performed -= useSkill2; 
            input.Movement.Attack3.performed -= useSkill3; 
            input.Movement.Dash.performed -= useDash;
        }else{
            input.MovementP2.Attack.Disable();
            input.MovementP2.Attack2.Disable();
            input.MovementP2.Attack3.Disable();
            input.MovementP2.Dash.Disable();

            input.MovementP2.Attack.performed -= useSkill1;
            input.MovementP2.Attack2.performed -= useSkill2;
            input.MovementP2.Attack3.performed -= useSkill3;
            input.MovementP2.Dash.performed -= useDash;
        }
    }

    public Skill getBasicAttack(){
        return skill1;
    }

    public Skill getSpecialAttack(){
        return skill2;
    }

    public Skill getDash(){
        return dash;
    }
}
