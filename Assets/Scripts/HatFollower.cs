using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatFollower : MonoBehaviour
{
    private Transform hatPosition;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        hatPosition = transform.parent.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(new Vector2(hatPosition.position.x, hatPosition.position.y));
    }
}
