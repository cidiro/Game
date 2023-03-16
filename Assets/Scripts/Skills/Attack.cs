﻿using UnityEngine;

namespace Skills
{
    public abstract class Attack : Skill
    {
        [SerializeField] protected int attackDamage = 20;
        [SerializeField] protected float attackRange = 0.5f;

        [SerializeField] protected Transform attackPoint;
        [SerializeField] protected LayerMask enemyLayers;
        
        new protected void Start() {
            base.Start();
            Debug.Log("Starting the Attack class...");
            //enemyLayers = gameObject.layer; Creo que esto la pone como Default aunque tengas un Serialize
            attackPoint = GetComponent<Transform>();
        }
        
        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
                return;
        
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

    }
}