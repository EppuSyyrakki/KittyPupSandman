using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 10f;
    public float fadeOutTime = 1.5f;
    [HideInInspector] public Animator state;
    [HideInInspector] public float lastPositionX;
    
    private bool movingRight;   
    private SpriteRenderer[] sr;
    private bool dead = false;

    // These are set in LightWeapon.cs if this is hit by the Light trigger
    [HideInInspector] public bool escaping;
    [HideInInspector] public Vector2 escapeDirection;

    FMOD.Studio.EventInstance boo;

    void Start()
    {
        state = GetComponent<Animator>();
        sr = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        LocationBasedFlip();
        state.SetBool("Escaping", escaping);

        if (dead)
        {
            Destroy(gameObject);
        }

        lastPositionX = transform.position.x;
    }

    private void LocationBasedFlip() 
    {
        if (lastPositionX < transform.position.x) movingRight = true;
        else movingRight = false;

        if (movingRight && transform.localScale.x != 1)
            transform.localScale = new Vector3(1, 1, 1);
        else if (!movingRight && transform.localScale.x != -1)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    IEnumerator FadeOut()
    {
        for (float ft = 1f; ft >= 0; ft -= 0.1f)
        {
            foreach (SpriteRenderer r in sr)
            {
                Color c = r.color;
                c.a = ft;
                r.color = c;
            }
            
            if (ft <= 0.1)
            {
                dead = true;
                StopEnemySound();
            }              

            if (!escaping)
            {
                CancelFade();
                break;
            } 
            else
                yield return new WaitForSeconds(fadeOutTime / 10);
        }
    }

    public void CancelFade()
    {
        foreach (SpriteRenderer r in sr)
        {
            Color c = r.color;
            c.a = 1f;
            r.color = c;
        }
    }

    public void PlayEnemySound(string path)
    {
        boo = FMODUnity.RuntimeManager.CreateInstance(path);
        //Debug.Log("the enemy: " + name );
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(boo, transform, GetComponent<Rigidbody2D>());
        boo.start();
        boo.release();
    }

    public void StopEnemySound()
    {
        boo.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
