using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    public bool isGrounded { get; private set; }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" && !isGrounded)
            isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = false;
    }
}
