using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController ec = collision.GetComponent<EnemyController>();
            Vector2 direction = (transform.position - collision.transform.position) * -Vector2.one;
            ec.escapeDirection = direction.normalized;
            ec.escaping = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().escaping = false;
        }
    }
}
