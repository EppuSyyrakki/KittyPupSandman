using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savepoint : MonoBehaviour
{
    public static Vector2 currentlyActivated;
    public static bool _isActive;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(this.tag + " : Savepoint trigger enter 2d " + collision.name);

        if (collision.tag == "Player")
            _isActive = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            Debug.Log("Savepoint triggered by: " + collision.tag + " | Game saved!");

            ActivateSavepoint();
            _isActive = false;
        }

    }

    private void ActivateSavepoint()
    {
        currentlyActivated = this.transform.position;
    }
}
