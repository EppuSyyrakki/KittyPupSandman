using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject matti;
    public float xOffset;
    public float yOffset;
    public float zOffset;
    public float followSpeed;

    private Vector3 target;
    private PlayerInputController pc;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody2D mattiRB;

    // Start is called before the first frame update
    void Start()
    {
        pc = matti.GetComponent<PlayerInputController>();
        mattiRB = matti.GetComponent<Rigidbody2D>();
        GetXPosition();
        GetYPosition();
        transform.position = target;
        followSpeed /= 100f;
    }

    // Update is called once per frame
    void Update()
    {
        target = new Vector3(matti.transform.position.x, matti.transform.position.y, zOffset);
        GetXPosition();
        GetYPosition();
        velocity = mattiRB.velocity;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, followSpeed);
    }

    private void GetXPosition()
    {
        if (matti.transform.localScale.x > 0)
            target.x += xOffset;
        else
            target.x -= xOffset;
    }

    private void GetYPosition()
    {
        if (pc.LookingDown)
            target.y -= yOffset - 0.7f;
        else
            target.y += yOffset;
    }
}