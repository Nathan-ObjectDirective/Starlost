using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damageToGive = 1;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 hitDirection = other.gameObject.transform.position - transform.position;

        hitDirection = hitDirection.normalized;

        if (other.tag == "Player")
        {
            FindObjectOfType<HealthManager>().damagePlayer(damageToGive, hitDirection);
        }
    }
}
