using System;
using System.Collections;
using System.Collections.Generic;
using Skills;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Skill skill1;
    [SerializeField] private Skill skill2;
    [SerializeField] private Skill skill3;


    private void Start()
    {
        /*De Guille: Te lo dejo como nota aunque ya te dije, pero si quieres me encargo yo de "traducir" las cosas para que usen el input system.
        Pero de todas formas, para cuando sigas este script echa un ojo a player movement o en general como gestionar el tema de usar un input para cada jugador,
        porque atacaran con teclas distintas, y eso si se puede administrar desde un solo script (este) en vez de tener que usar un script para cada player mejor. 
        En general, que si lo puedes hacer lo mas generalizado posible (osea a poder ser que los jugadores compartan script), mejor*/
    }

    private void Update()
    {
        // con el nuevo input esto ya no funciona no?
        if (Input.GetKeyDown(KeyCode.F))
        {
            skill1.UseSkill();
        }
    }
}
