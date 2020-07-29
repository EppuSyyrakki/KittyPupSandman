using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    // this is the machine that controls Player object states that handle physics and animation by sending messages
    // to the Animator component in gameObject's child (Matti). called "state" in the behaviour script methods.
    [HideInInspector] public Animator state;
    [HideInInspector] public float currentSpeed;
    private Rigidbody2D rigidbody2d;
    private GameObject lampClone;
    private bool _vulnerable;
    private SpriteRenderer[] spriteRenderers;

    [Header("Movement attributes")]
    public float _maxSpeed;
    public float _fastSpeed;
    public float _slowSpeed;
    public float _speedWithLamp;   
    public float _jumpForce;    
    public float _floatingDrag;

    [Header("Combat attributes")]
    public float _vulnerableTime;
    public float _bumpForce;

    [Header("Prefabs and components")]
    public HitCheck hitCheck;
    public GroundedCheck groundedCheck;
    public GameObject umbrella;
    public Transform umbrellaPosition;
    public GameObject lamp;
    public Transform lampPosition;

    public bool LightDeployed { get; set; }

    public bool LookingDown { get; set; }

    void Start()
    {
        state = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        transform.position = SaveGame.Instance.GetPosFromMemory();      
    }

    void Update()
    {
        SetStateParameters();
        SetStateGrounded();
        SetLamp();

        if (hitCheck.IsHit)
        {           
            ResolveHits();           
        }

        if (Input.GetKeyDown(KeyCode.P))
            SaveGame.Instance.WriteFile(this.gameObject.transform.position);
    }

    private void SetStateParameters()
    {
        state.SetFloat("InputX", Input.GetAxis("Horizontal"));
        state.SetFloat("InputXAbs", Mathf.Abs(Input.GetAxis("Horizontal")));

        if (Input.GetButtonDown("Jump"))
            state.SetBool("InputJump", true);
        else if (Input.GetButtonUp("Jump"))
            state.SetBool("InputJump", false);

        if (Input.GetAxis("Vertical") > 0)
            state.SetBool("InputFloat", true);
        else if (Input.GetAxis("Vertical") <= 0)
            state.SetBool("InputFloat", false);          

        if (Input.GetAxis("Vertical") < 0)
            LookingDown = true;

        if (Input.GetAxis("Vertical") == 0)
            LookingDown = false;       
    }

    public void SetStateGrounded()
    {
        if (groundedCheck.isGrounded)
            state.SetBool("Grounded", true);
        else
            state.SetBool("Grounded", false);
    }

    private void SetLamp()
    {
        if (Input.GetButtonDown("Light") && !LightDeployed)
        {
            lampClone = Instantiate(lamp, lampPosition.position, transform.rotation, transform);
            LightDeployed = true;           
        }
        if (Input.GetButtonUp("Light") && LightDeployed)
        {
            Destroy(lampClone);
            LightDeployed = false;
        }
    }

    private void ResolveHits()
    {
        if (!_vulnerable)
        {
            Vector2 bump = new Vector2();

            if (hitCheck.HitLocation.x >= transform.position.x)
                bump.x = -_bumpForce;
            else
                bump.x = _bumpForce;
            
            rigidbody2d.AddForce(bump * _bumpForce * Time.deltaTime);
            PlayerVulnerable(0.5f);
            _vulnerable = true;          
        }
        else
        {
            Debug.Log("PLAYER IS DEAD");
            PlayerVulnerable(0f);
        }

        if (_vulnerable) Invoke("DisableVulnerability", _vulnerableTime);
    }

    private void PlayerVulnerable(float alpha)
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }
    }

    private void DisableVulnerability()
    {
        _vulnerable = false;
        PlayerVulnerable(1f);
    }

    void OnDisable()
    {
        if (lampClone != null) Destroy(lampClone);
    }
}
