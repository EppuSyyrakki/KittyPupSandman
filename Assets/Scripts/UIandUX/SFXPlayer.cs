using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField]
    string hurtSound;
    private bool _isHurtSound;

    [SerializeField]
    string dieSound;
    private bool _isDieSound;

    public static SFXPlayer Instance { get; private set; }

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
        ValidateSounds();

        if (Instance == null)
            Instance = this;
        else
            Debug.LogWarning("Warning! Multiple " + this + "in the scene");
    }

    private void ValidateSounds()
    {
        if (hurtSound != null)
            _isHurtSound = true;
        if (dieSound != null)
            _isDieSound = true;
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

    public void PlayDie()
    {
        if (_isDieSound)
            FMODUnity.RuntimeManager.PlayOneShot(dieSound, GetComponent<Transform>().position);
    }
}

