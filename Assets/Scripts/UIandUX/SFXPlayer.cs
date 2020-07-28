using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    private string matti = "matti";

    [SerializeField]
    string hurtSound;
    private bool _isHurtSound;

    private void OnEnable()
    {
        EventManager.onPlayerDamageEvent += PlayerHurt;
    }


    private void OnDisable()
    {
        EventManager.onPlayerDamageEvent -= PlayerHurt;
    }

    private void OnDestroy()
    {
        EventManager.onPlayerDamageEvent -= PlayerHurt;
    }

    private void Awake()
    {
        if (hurtSound != null)
            _isHurtSound = true;
    }

    private void PlayFootstepsPlayer(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

    private void PlayJumpPlayer(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

    private void PlayerHurt()
    {
        if (_isHurtSound)
            FMODUnity.RuntimeManager.PlayOneShot(hurtSound, GetComponent<Transform>().position);     
    }
}

