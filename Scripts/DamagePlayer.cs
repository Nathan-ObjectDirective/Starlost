using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage = 1;

	// Update is called once per frame
	void Update ()
    {
        // rotate the tree object
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // set a direction equal to the opposite direction that the player approached the object.
        Vector3 directionToKnockback = other.gameObject.transform.position - transform.position;

        // normalise the firection
        directionToKnockback = directionToKnockback.normalized;

        if (other.tag == "Player")
        {
            // damage the player with a direction to knock the player back
            FindObjectOfType<HealthManager>().damagePlayer(damage, directionToKnockback);
        }
    }
}
