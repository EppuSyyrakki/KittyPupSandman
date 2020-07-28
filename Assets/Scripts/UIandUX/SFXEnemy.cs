using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXEnemy : MonoBehaviour
{
    [SerializeField]
    private string _patrolSound;
    private bool _isPatrol;

    [SerializeField]
    private string _attackSound;
    private bool _isAttack;

    private void OnEnable()
    {
        EventManager.onPlayerDamageEvent += CheckAttackType;
    }

    private void OnDisable()
    {
        EventManager.onPlayerDamageEvent -= CheckAttackType;
    }

    private void OnDestroy()
    {
        EventManager.onPlayerDamageEvent -= CheckAttackType;
    }

    private void Awake()
    {
        SoundsInit();
    }
        private void SoundsInit()
    {
        if (_patrolSound != null)
            _isPatrol = true;

        if (_attackSound != null)
            _isAttack = true;
    }
    
    public void PlayPatrolEnemy()
    {
        if (_isPatrol)
        {
            FMODUnity.RuntimeManager.PlayOneShot(_patrolSound, GetComponent<Transform>().position);
        }
        else
            Debug.LogWarning("No enemy patrol sound available.");
    }

    private void CheckAttackType()
    {
        if (name == EnemyController.GetAttackSoundtype() && _isAttack)
        {
            FMODUnity.RuntimeManager.PlayOneShot(_attackSound, GetComponent<Transform>().position);
        }
    }
}
