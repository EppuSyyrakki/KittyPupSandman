using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    [Header("Character State Controller:")] public Animator _state; // State machine instance

    public void SetGrounded()
    {
        _state.SetBool("Grounded", true);
    }
}
