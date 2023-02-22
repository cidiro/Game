using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingController : MonoBehaviour
{
    
    [SerializeField] private GameObject player;
    
    private SpriteRenderer playerSprite;
    private Transform playerTransform;
    private SpriteRenderer sprite;

    private void Start()
    {
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerTransform = player.GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        if (playerSprite.flipX)
        {
            sprite.flipX = true;
            transform.position = new Vector3(playerTransform.position.x - 1.66f, playerTransform.position.y + 0.06f, 0f);
        }
        else
        {
            sprite.flipX = false;
            transform.position = new Vector3(playerTransform.position.x + 1.66f, playerTransform.position.y + 0.06f, 0f);
        }
    }
}
