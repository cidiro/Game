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
    private InputsPlayer input;

    private void Start()
    {
        /*De Guille: Te lo dejo como nota aunque ya te dije, pero si quieres me encargo yo de "traducir" las cosas para que usen el input system.
        Pero de todas formas, para cuando sigas este script echa un ojo a player movement o en general como gestionar el tema de usar un input para cada jugador,
        porque atacaran con teclas distintas, y eso si se puede administrar desde un solo script (este) en vez de tener que usar un script para cada player mejor. 
        En general, que si lo puedes hacer lo mas generalizado posible (osea a poder ser que los jugadores compartan script), mejor*/
        
        //Lo hago como para el jugador1, es decir, si fuera su propio script, pero hay que cambiarlo para que compartan mejor

      
        input = new InputsPlayer();
        input.Movement.Attack.Enable();
        input.Movement.Attack2.Enable(); //Activas los ataques de el player1
        input.Movement.Attack3.Enable();

        input.Movement.Attack.performed += useSkill1;
        input.Movement.Attack2.performed += useSkill2; //Subscribes las funciones use skill a cada input, por lo que si se pulsa la tecla vinculada a Attack
        input.Movement.Attack3.performed += useSkill3; //se llamara a useSkill1, Attack2 llamarĂ¡ a useSkill2, etc...
    }

    private void useSkill1(InputAction.CallbackContext context){ //El callback context solo esta porque hace falta para poder subscribir la funcion, pero no
        skill1.UseSkill();                                       //hacemos nada con el a parte de declararlo como entrada
    }
    private void useSkill2(InputAction.CallbackContext context){
        skill2.UseSkill();
    }
    private void useSkill3(InputAction.CallbackContext context){
        skill3.UseSkill();
    }
}
