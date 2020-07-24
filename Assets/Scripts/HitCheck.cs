using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    public bool IsHit { get; private set; }
    public string _tagName;
    public Vector2 HitLocation { get; private set; }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_tagName))
        {
            IsHit = true;
            HitLocation = collision.GetContact(0).point;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_tagName))
        {
            IsHit = false;
        }
    }
}
