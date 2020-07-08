using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Sprite patrolSprite;
    public Sprite alertSprite;
    public Transform[] waypoints;
    public float speed = 10f;

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
