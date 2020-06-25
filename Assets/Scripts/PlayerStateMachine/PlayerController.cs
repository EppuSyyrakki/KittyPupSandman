using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    public Sprite idleSprite;
    public Sprite runSprite;
    public Sprite jumpSprite;

    [HideInInspector] public SpriteRenderer spriteRenderer;   // temporary tool before we get animations

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        _state.SetFloat("Horizontal", Input.GetAxis("Horizontal")); // send Axis to PlayerStateController parameter

        if (Input.GetAxis("Horizontal") != 0)
        {
            _state.SetTrigger("Run"); // name of a Trigger parameter in PlayerStateController
        } 

        if (Input.GetButtonDown("Jump"))
        {
            _state.SetTrigger("Jump");
        }
    }
}
