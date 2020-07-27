using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    [SerializeField]
    private string _patrolSound;
    private bool _isPatrol;

    [SerializeField]
    private string _attackSound;
    private bool _isAttack;

    private void OnEnable()
    {
        EventManager.onKillEnemySoundEvent += StopAllEnemySounds;
        EventManager.onGroundEnemyAttackEvent += GroundEnemyAttack;
        EventManager.onCrawlingEnemyAttackEvent += CrawlingEnemyAttack;
        //EventManager.onAirEnemyAttackEvent += ;
        EventManager.onPlayerDamageEvent += PlayerHurt;
    }


    private void OnDisable()
    {
        EventManager.onKillEnemySoundEvent -= StopAllEnemySounds;
        EventManager.onGroundEnemyAttackEvent -= GroundEnemyAttack;
        EventManager.onCrawlingEnemyAttackEvent -= CrawlingEnemyAttack;
        //EventManager.onAirEnemyAttackEvent -= ;
        EventManager.onPlayerDamageEvent -= PlayerHurt;
    }

    private void OnDestroy()
    {
        EventManager.onKillEnemySoundEvent -= StopAllEnemySounds;
        EventManager.onGroundEnemyAttackEvent -= GroundEnemyAttack;
        EventManager.onCrawlingEnemyAttackEvent -= CrawlingEnemyAttack;
        //EventManager.onAirEnemyAttackEvent -= ;
        EventManager.onPlayerDamageEvent -= PlayerHurt;
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

    public void CrawlingEnemyAttack()
    {
        if (_isAttack && name.Contains("crawl"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(_attackSound, GetComponent<Transform>().position);
        }
    }

    public void GroundEnemyAttack()
    {
        if (_isAttack && name.Contains("ground"))
        {

            FMODUnity.RuntimeManager.PlayOneShot(_attackSound, GetComponent<Transform>().position);
        }
    }

    private void StopAllEnemySounds() // this is not in action at the moment
    {
        Debug.Log("Enemy died");
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
        string path = "event:/matti/hurt";
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
        Debug.Log("OUUUUUUUcH!");
    }
}
