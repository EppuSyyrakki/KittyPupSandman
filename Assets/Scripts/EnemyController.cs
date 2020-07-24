using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 10f;
    public bool variableSpeed;
    public float fadeOutTime = 1.5f;
    [HideInInspector] public Animator state;
    [HideInInspector] public float lastPositionX;
    
    private bool movingRight;   
    private SpriteRenderer[] sr;
    private bool dead = false;

    public static bool _isKillSound { get; set; }
    public static bool _isAttackSoundGround { get; private set; }
    public static bool _isAttackSoundAir { get; private set; }

    // These are set in LightWeapon.cs if this is hit by the Light trigger
    [HideInInspector] public bool escaping;
    [HideInInspector] public Vector2 escapeDirection;

    void Start()
    {
        state = GetComponent<Animator>();
        sr = GetComponentsInChildren<SpriteRenderer>();
        _isKillSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        LocationBasedFlip();
        state.SetBool("Escaping", escaping);

        if (dead)
        {
            Destroy(gameObject);
            _isKillSound = true;
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
        for (float alpha = 1f; alpha >= 0; alpha -= 0.05f)
        {
            foreach (SpriteRenderer r in sr)
            {
                Color c = r.color;
                c.a = alpha;
                r.color = c;
            }
            
            if (alpha <= 0.1)
            {
                dead = true;
            }              

            if (!escaping)
            {
                CancelFade(alpha);
                break;
            } 
            else
                yield return new WaitForSeconds(fadeOutTime * 0.05f);
        }
    }

    private void CancelFade(float alpha) => StartCoroutine("FadeIn", alpha);

    IEnumerator FadeIn(float alphaStart)
    {
        for (float alpha = alphaStart; alpha <= 1; alpha += 0.05f)
        {
            foreach (SpriteRenderer r in sr)
            {
                Color c = r.color;
                c.a = alpha;
                r.color = c;
            }
            yield return new WaitForSeconds(fadeOutTime * 0.05f);
        }
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (name.Contains("Shadow_ground Variant") && collision.collider.tag == "Player")
            _isAttackSoundGround = true;

        else if (name.Contains("Shadow_fly Variant") && collision.collider.tag == "Player")
            _isAttackSoundAir = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (name.Contains("Shadow_ground Variant") && collision.collider.tag == "Player")
            _isAttackSoundGround = false;

        else if (name.Contains("Shadow_fly Variant") && collision.collider.tag == "Player")
            _isAttackSoundAir = false;
    }
}
