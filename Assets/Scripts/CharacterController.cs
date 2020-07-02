using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    [Header("Character State Controller:")] public Animator _state; // State machine instance

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground") 
        {
            _state.SetBool("Grounded", true);
        }
    }
}
