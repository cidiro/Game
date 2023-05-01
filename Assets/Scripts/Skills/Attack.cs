using UnityEngine;

namespace Skills
{
    public abstract class Attack : Skill
    {
        [SerializeField] protected int attackDamage = 20;
        [SerializeField] protected float attackRange = 0.5f;

        [SerializeField] protected Transform attackPoint; //Where the center of the attack is, if the center point of the attack is the center of the player body 
                                                          //it´s 0, but if the attack happens at a side of the player this should have the value to indicate 
                                                          //where it is located relatively to the player.
        [SerializeField] protected LayerMask enemyLayers; //Layer in which we check for impacts when attacking
        
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