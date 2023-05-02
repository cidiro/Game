using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonHoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    //This GameObject only contains the Text we want to show when hovering over the button, thats
    //why we do SetActive to the GameObject itself. If we were to have cases where the textWindow could contain
    //things apart from the text and font we would need to change the .setActive() for .GetComponent<TestMeshProUGUI>().SetActive()
    [SerializeField] private GameObject textWindow;

    private void Start() {
        textWindow.SetActive(false);    
    }

    public void OnPointerEnter(PointerEventData data){
        textWindow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData data){
        textWindow.SetActive(false);
    }

}
