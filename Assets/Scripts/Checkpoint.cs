using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            activated = true;
            GameManager.Instance.UpdateCheckpoint(this.transform.position);
            // You can also play a sound or animation to indicate activation.
        }
    }
}
