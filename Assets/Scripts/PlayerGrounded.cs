using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{
    public PlayerInputController pc;
    // Start is called before the first frame update

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            pc.SetGrounded();
        }
    }
}
