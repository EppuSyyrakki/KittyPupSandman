using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    public Sprite patrolSprite;
    public Sprite alertSprite;

    [HideInInspector] public SpriteRenderer spriteRenderer;   // temporary tool before we get animations

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
