using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    public GameObject objectToActivate; // Public GameObject to activate

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check collision with object tagged as "Player"
        {
            ActivateObject();
        }
    }

    private void ActivateObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
