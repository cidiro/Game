using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionControl : MonoBehaviour
{
    [SerializeField] private GameObject transition;

    private void Start() {
        transition.gameObject.SetActive(true);

        LeanTween.alpha(transition, 1, 0);
        LeanTween.alpha(transition, 0, 0.5f).setOnComplete(() =>{
            transition.SetActive(false);
        });
    }
}
