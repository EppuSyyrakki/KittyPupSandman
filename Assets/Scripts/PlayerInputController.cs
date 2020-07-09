using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    // this is the machine that controls Player object states that handle physics and animation by sending messages
    // to the Animator component in gameObject's child (Matti). called "state" in the behaviour script methods.
    [HideInInspector] public Animator state;
        
    public float maxSpeed;
    public float groundSpeed;
    public float jumpForce;
    public float airborneSpeed;
    public float runToWalkThreshold;


    void Start()
    {
        state = GetComponent<Animator>();
        transform.position = SaveGame.Instance.GetPosFromMemory();
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // set the state machine Horizontal parameter every frame it's touched so it doesn't need to know about input.
        state.SetFloat("Horizontal", Input.GetAxis("Horizontal"));

        if (Input.GetAxis("Horizontal") != 0)
        {           
            if (state.GetBool("Grounded")) 
                state.SetTrigger("Run");
        } 

        if (Input.GetButtonDown("Jump") && state.GetBool("Grounded"))
        {
            state.SetTrigger("Jump");
        }

        if (Input.GetButtonDown("Light") && state.GetBool("Grounded"))
        {
            // state.SetTrigger("Fire"); no such trigger yet
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveGame.Instance.SetPosVec(this.gameObject.transform.position);
            
        }
    }

    public void SetGrounded()   // script PlayerGrounded.cs in the player's legs uses this on collisions with Ground
    {
        state.SetBool("Grounded", true);
    }
}
