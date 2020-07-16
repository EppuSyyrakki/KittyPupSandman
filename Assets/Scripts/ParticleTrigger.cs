using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    private ParticleSystem ps;
    [Range(0,2)] public float _triggerDelay;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Invoke("TriggerParticles", _triggerDelay);
    }

    private void TriggerParticles()
    {
        ps.Play(false);
    }
}
