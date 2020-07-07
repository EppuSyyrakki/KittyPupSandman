using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Character State Controller:")] public Animator _state; // State machine instance

    public float maxSpeed;
    public float groundSpeed;
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
        _state.SetFloat("Horizontal", Input.GetAxis("Horizontal"));

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

    public void SetGrounded()   // script PlayerGrounded.cs in the player's legs uses this
    {
        _state.SetBool("Grounded", true);
    }
}
