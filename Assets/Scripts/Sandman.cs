using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandman : MonoBehaviour
{
    private PlayerState _state;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    public Sprite idleSprite;
    public Sprite runSprite;
    public Sprite jumpSprite;

    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _runSpeed;
    private bool _grounded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _state = PlayerState.Idling;
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_grounded) HandleInput();
        Debug.Log(_grounded);
    }

    private void HandleInput()
    {
        switch (_state)
        {
            case PlayerState.Idling:
                _sr.sprite = idleSprite;
                
                if (Input.GetButtonDown("Jump") && _grounded)
                {
                    _state = PlayerState.Jumping;                 
                }

                if (Input.GetAxis("Horizontal") != 0) {
                    _state = PlayerState.Running;                                      
                }
                break;

            case PlayerState.Running:
                _sr.sprite = runSprite;
                _rb.AddForce(Vector2.right * Input.GetAxis("Horizontal") * _runSpeed * Time.deltaTime);

                if (Input.GetAxis("Horizontal") == 0)
                {
                    _state = PlayerState.Idling;
                }

                if (Input.GetButtonDown("Jump") && _grounded)
                {
                    _state = PlayerState.Jumping;
                }
                break;

            case PlayerState.Jumping:
                _sr.sprite = jumpSprite;
                _rb.AddForce(Vector2.up * _jumpSpeed * Time.deltaTime);

                if (_grounded)
                    _state = PlayerState.Idling;
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") _grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") _grounded = false;
    }
}
