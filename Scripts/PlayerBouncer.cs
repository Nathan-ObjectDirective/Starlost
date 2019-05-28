using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBouncer : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")

        {   // tell the player to bounce on next collision with the ground
            FindObjectOfType<PlayerController>().bounce();
        }
    }
}
