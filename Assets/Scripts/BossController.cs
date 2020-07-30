using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [HideInInspector] public Animator state;
    [HideInInspector] public bool attacking;
    public bool lit;
    public GameObject matti;
    public GameObject[] claws;
    public float _turningDelay;
    public int _lives;
    public int fadeOutTime;

    private float _distanceFromPlayer;
    private SpriteRenderer[] sr;

    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<Animator>();
        sr = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _distanceFromPlayer = Mathf.Abs(transform.position.x - matti.transform.position.x);

        Vector3 target = new Vector3(matti.transform.position.x, transform.position.y, 0);
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime);

        if (_distanceFromPlayer < 11 && !attacking)
        {
            if (Random.Range(0f, 1f) <= 0.5)
                state.SetTrigger("Attack2");
            else
                state.SetTrigger("Attack");
        }
        PlayerBasedFlip();

        if (_lives <= 0)
        {
            // end game
        }
    }

    public void EnableColliders(bool arg)
    {
        foreach (GameObject obj in claws)
        {
            CircleCollider2D collider = obj.GetComponent<CircleCollider2D>();
            collider.enabled = arg;
        }
    }

    public void PlayerBasedFlip()
    {
        if (matti.transform.position.x > transform.position.x)
            Invoke("TurnRight", _turningDelay);
        else if (matti.transform.position.x < transform.position.x)
            Invoke("TurnLeft", _turningDelay);
    }

    private void TurnRight() => transform.localScale = new Vector3(-1, 1, 1);

    private void TurnLeft() => transform.localScale = new Vector3(1, 1, 1);

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
                _lives--;
                CancelFade(0.6f);
            }

            if (!lit)
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
        for (float alpha = alphaStart; alpha <= 1; alpha += 0.1f)
        {
            foreach (SpriteRenderer r in sr)
            {
                Color c = r.color;
                c.a = alpha;
                r.color = c;
            }
            yield return new WaitForSeconds(fadeOutTime * 0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lamp"))
        {
            lit = true;
            StartCoroutine("FadeOut");
        }           
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lamp"))
            lit = false;
    }
}
