using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [HideInInspector] public Animator state;
    public GameObject matti;

    private float _distanceFromPlayer;

    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _distanceFromPlayer = Mathf.Abs(transform.position.x - matti.transform.position.x):
        PlayerBasedFlip();
        Debug.Log(_distanceFromPlayer);
    }

    private void PlayerBasedFlip()
    {
        if (matti.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }
}
