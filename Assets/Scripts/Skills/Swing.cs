using UnityEngine;

namespace Skills
{
    public class Swing : Attack
    {
        private bool swingState;

        //X and Y from the center of the swing attack, these are relative to the player position.
        private float localX, localY; 
        
        //Reference to the SpriteRenderer of the player that performed the swing.
        private SpriteRenderer playerSprite;
        private float lastUse=0;

        //Sound to be played when the swing is done.
        [SerializeField] private AudioSource swingDone;

        //Sound to be played when a swing connects.
        [SerializeField] private AudioSource swingHit;
        new private void Start()
        {
            base.Start();
            Debug.Log("Starting the Swing class...");
            
            playerSprite = player.GetComponent<SpriteRenderer>();
            localX = Mathf.Abs(transform.localPosition.x);
            localY = transform.localPosition.y;
        }


        //This will check if which way we are looking, and in order of where we look it will change the attacks position, this because
        //if we are looking to the left we want to attack to be to the left of the player, so -localX, or if we are looking to the right
        //we want the attack to be at the right side of the player, so +localX
        private void Update()
        {
            if (playerSprite.flipX)
            {
                sprite.flipX = true;
                attackPoint.transform.position = new Vector3(transform.parent.position.x - localX, transform.parent.position.y + localY, 0f);
            }
            else
            {
                sprite.flipX = false;
                attackPoint.transform.position = new Vector3(transform.parent.position.x + localX, transform.parent.position.y + localY, 0f);
            }
        }

        public override void UseSkill()
        {
            //Check if enough time has went by since the last time we used the swing
            if (Time.time-lastUse >= cooldown)
            {
                lastUse=Time.time;
                swingDone.Play();
                Debug.Log("Swing");
                if (swingState)
                    animator.SetTrigger("SwingA");
                else
                    animator.SetTrigger("SwingB");

                swingState = !swingState;

                //In hitEnemies, store the Colliders of all the GameObjects that are in the range of the attack.
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, enemyLayers);

                //For each Collider check if it belongs to a player, and if it does aply damage to that player.
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("collider");
                    if (enemy != playerCollider){
                        swingHit.Play();
                        enemy.GetComponent<Player>().TakeDamage(attackDamage);
                    }
                }
            }
        }
    }
}
