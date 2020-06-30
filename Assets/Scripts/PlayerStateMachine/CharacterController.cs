﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    [Header("Player State Controller:")] public Animator _state; // State machine instance

    [Range(2, 9)] public float _runSpeed = 2;
    [Range(0.01f, 0.1f)] public float _jumpPowerX = 0.01f; // Horizontal jump power needs to be really small
    [Range(1, 3)] public float _jumpPowerY = 1;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _state.SetBool("Grounded", true);
        }
    }
}
