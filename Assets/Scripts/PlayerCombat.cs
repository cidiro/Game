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
