using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    public bool isGrounded { get; private set; }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!isGrounded)
        {
            if (collision.CompareTag("Ground")) // or "Enemy" if we want to have player walking on enemies.
                isGrounded = true;
        }      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
            isGrounded = false;
    }
}
