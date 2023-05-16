using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour
{

    [SerializeField]private Color alreadySelectedColor;
    [SerializeField]private Button boton;

    public void changeColor(){
        ColorBlock colors = boton.colors;
        colors.normalColor = alreadySelectedColor;
        colors.selectedColor = alreadySelectedColor;
        colors.pressedColor = alreadySelectedColor;
        boton.colors = colors; 
    }
}
