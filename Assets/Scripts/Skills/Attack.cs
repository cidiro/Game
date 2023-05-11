using UnityEngine;

namespace Skills
{
    public abstract class Attack : Skill
    {
        [SerializeField] protected int attackDamage;
        [SerializeField] protected float attackRange;

        [SerializeField] protected Transform attackPoint; //Where the center of the attack hitbox is
        [SerializeField] protected LayerMask enemyLayers; //Layer in which we check for impacts when attacking
        
        //Where the center of the attack hitbox is located relatively to the player coordinates.
        protected float localX, localY;
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