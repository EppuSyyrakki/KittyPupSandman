using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    // this is the machine that controls Player object states that handle physics and animation by sending messages
    // to the Animator component in gameObject's child (Matti). called "state" in the behaviour script methods.
    [HideInInspector] public Animator state;

    public float maxSpeed;
    public float fastSpeed;
    public float slowSpeed;
    public float jumpForce;    
    public float floatingDrag;
    public GroundedCheck groundedCheck;
    public GameObject umbrella;
    public Transform umbrellaPosition;
    public GameObject lamp;
    public Transform lampPosition;

    public bool LightDeployed { get; set; }

    void Start()
    {
        state = GetComponent<Animator>();
        transform.position = SaveGame.Instance.GetPosFromMemory();
        LightDeployed = false;
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // set the state machine input parameters every frame so it doesn't have to know about input
        SetStateFloats();
        SetStateBools();
        SetStateGrounded();

        if (Input.GetKeyDown(KeyCode.P))
            SaveGame.Instance.SetPosVec(this.gameObject.transform.position);
    }
    private void SetStateFloats()
    {
        state.SetFloat("InputX", Input.GetAxis("Horizontal"));
        state.SetFloat("InputXAbs", Mathf.Abs(Input.GetAxis("Horizontal")));
    }

    private void SetStateBools()
    {
        if (Input.GetButtonDown("Jump"))
            state.SetBool("InputJump", true);
        if (Input.GetButtonUp("Jump"))
            state.SetBool("InputJump", false);

        if (Input.GetButtonDown("Float")) 
            state.SetBool("InputFloat", true);       
        if (Input.GetButtonUp("Float"))
            state.SetBool("InputFloat", false);

        if (Input.GetButtonDown("Light"))
            state.SetBool("InputLight", true);
        if (Input.GetButtonUp("Light"))
            state.SetBool("InputLight", false);
    }

    public void SetStateGrounded()
    {
        if (groundedCheck.isGrounded)
            state.SetBool("Grounded", true);
        else
            state.SetBool("Grounded", false);
    }
}
