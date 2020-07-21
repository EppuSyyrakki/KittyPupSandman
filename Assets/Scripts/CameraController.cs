using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject matti;
    public float verticalOffset;
    public float horizontalOffset;
    public float depthOffset;

    private Vector3 target;
    private PlayerInputController pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = matti.GetComponent<PlayerInputController>();
    }

    // Update is called once per frame
    void Update()
    {
        target = new Vector3(matti.transform.position.x, matti.transform.position.y, depthOffset);
        GetVerticalPosition();
        GetHorizontalPosition();

        transform.position = target;
    }

    private void GetVerticalPosition()
    {
        if (matti.transform.localScale.x > 0)   // TODO smooth out camera movement on direction change
        {
            target.x += verticalOffset;
        }
        else
        {
            target.x -= verticalOffset;
        }
    }

    private void GetHorizontalPosition()
    {
        if (pc.LookingDown) // TODO smooth out camera movement on pressing down
        {
            target.y -= horizontalOffset;
        }
        else
        {
            target.y += horizontalOffset;
        }
    }
}
