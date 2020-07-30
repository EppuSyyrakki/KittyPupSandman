using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightWeapon : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent(out EnemyController ec))
            {
                Vector2 direction = (transform.position - collision.transform.position) * -Vector2.one;
                ec.escapeDirection = direction.normalized;
                ec.escaping = true;
            }          
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent(out EnemyController ec))
            {
                ec.escaping = false;
            }            
        }
    }
}
