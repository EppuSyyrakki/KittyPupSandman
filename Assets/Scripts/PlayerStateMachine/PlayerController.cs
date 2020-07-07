using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    public float maxSpeed;
    public float acceleration;
    public float jumpForce;
    public float airborneSpeed;


    void Start()
    {
        transform.position = SaveGame.Instance.GetPosFromMemory();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // the _state object is from CharacterController superclass
        _state.SetFloat("Horizontal", Input.GetAxis("Horizontal")); // send Axis to PlayerStateController parameter

        if (Input.GetAxis("Horizontal") != 0)
        {
            _state.SetTrigger("Run"); // name of a Trigger parameter in PlayerStateController
        } 

        if (Input.GetButtonDown("Jump"))
        {
            _state.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveGame.Instance.SetPosVec(this.gameObject.transform.position);
            
        }
    }
}
