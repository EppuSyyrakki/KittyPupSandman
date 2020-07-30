using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    public static bool _playerDamage { get; set; }
    public bool IsHit { get; private set; }
    public string _tagName;
    public Vector2 HitLocation { get; private set; }

    public static HitCheck Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogWarning("Warning! Multiple " + this + " in the scene");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_tagName))
        {
            IsHit = true;
            _playerDamage = true;
            HitLocation = collision.GetContact(0).point;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_tagName))
        {
            IsHit = false;
            _playerDamage = false;
        }
    }

    public bool GetIsHit()
    {
        return IsHit;
    }

    public void SetIsHit()
    {
        if (IsHit)
            IsHit = false;
    }

    public void SetIsDamage()
    {
        if (_playerDamage)
            _playerDamage = false;
    }
}
