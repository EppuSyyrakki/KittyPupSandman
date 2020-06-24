using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player State Controller:")] public Animator _state;    // State machine instance
    
    // [Header("Player Animation Controller:")] public Animator _animation;
    // if we use a different animation controller for graphics, it goes here. 
    // May be possible with the same State Controller as above

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        // HandleGraphics();
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _state.SetBool("Grounded", true);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _state.SetBool("Grounded", false);
        }
    }

}
