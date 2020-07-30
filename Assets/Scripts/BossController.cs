using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [HideInInspector] public Animator state;
    public GameObject matti;
    public GameObject[] claws;
    public float _turningDelay;

    private float _distanceFromPlayer;

    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _distanceFromPlayer = Mathf.Abs(transform.position.x - matti.transform.position.x);
        Debug.Log(_distanceFromPlayer);

        if (_distanceFromPlayer < 8)
        {
            EnableColliders(true);
            state.SetTrigger("Attack");
        }
        PlayerBasedFlip();
    }

    private void EnableColliders(bool arg)
    {
        foreach (GameObject obj in claws)
        {
            CircleCollider2D collider = obj.GetComponent<CircleCollider2D>();
            collider.enabled = arg;
        }
    }

    private void PlayerBasedFlip()
    {
        if (matti.transform.position.x > transform.position.x)
            Invoke("TurnRight", _turningDelay);
        else if (matti.transform.position.x < transform.position.x)
            Invoke("TurnLeft", _turningDelay);
    }

    private void TurnRight() => transform.localScale = new Vector3(-1, 1, 1);

    private void TurnLeft() => transform.localScale = new Vector3(1, 1, 1);
}
