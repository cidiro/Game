using UnityEngine;

namespace Skills
{
    public class Swing : Attack
    {
        private bool swingState;
        private float localX, localY;
        
        private SpriteRenderer playerSprite;
        private float lastUse=0;
        [SerializeField] private AudioSource swingDone;
        [SerializeField] private AudioSource swingHit;
        new private void Start()
        {
            base.Start();
            Debug.Log("Starting the Swing class...");
            
            playerSprite = player.GetComponent<SpriteRenderer>();
            localX = Mathf.Abs(transform.localPosition.x);
            localY = transform.localPosition.y;
        }

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
                //Debug.Log(LayerMask.LayerToName(enemyLayers)); btw, asi se ve el nombre de una layer
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, enemyLayers);
                Debug.Log(hitEnemies.Length);
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
