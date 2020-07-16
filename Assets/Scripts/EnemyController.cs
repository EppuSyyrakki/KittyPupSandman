using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 10f;
    public float fadeOutTime = 1.5f;
    [HideInInspector] public Animator state;
    [HideInInspector] public Vector2 lastPosition;
    
    private SpriteRenderer[] sr;
    private bool dead = false;

    // These are set in LightWeapon.cs if this is hit by the Light trigger
    [HideInInspector] public bool escaping;
    [HideInInspector] public Vector2 escapeDirection;

    void Start()
    {
        state = GetComponent<Animator>();
        sr = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        state.SetBool("Escaping", escaping);

        if (dead)
        {
            Destroy(gameObject);
        }
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
}
