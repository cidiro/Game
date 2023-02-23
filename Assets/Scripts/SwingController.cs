using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingController : MonoBehaviour
{
    
    [SerializeField] private GameObject player;
    
    private SpriteRenderer playerSprite;
    private Transform playerTransform;
    private SpriteRenderer sprite;
    
    private float localX, localY;

    private void Start()
    {
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerTransform = player.GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();

        localX = Mathf.Abs(transform.localPosition.x);
        localY = transform.localPosition.y;
    }
    
    private void Update()
    { 
        if (playerSprite.flipX)
        {
            sprite.flipX = true;
            transform.position = new Vector3(playerTransform.position.x - localX, playerTransform.position.y + localY, 0f);
        }
        else
        {
            sprite.flipX = false;
            transform.position = new Vector3(playerTransform.position.x + localX, playerTransform.position.y + localY, 0f);
        }
    }
}
