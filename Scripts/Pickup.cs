using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int amountOfPlatformsCanSpawnPerPickup = 1;
    public bool isGoal;

    public GameObject pickupParticleEffect;
    private Renderer mesh;
    private BoxCollider collider;


	// Use this for initialization
	void Start ()
    {
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();
    }
	
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            FindObjectOfType<GameManager>().addAvailablePlatforms(1);

            Instantiate(pickupParticleEffect, transform.position, transform.rotation);
            
            if (!isGoal)
            {
                Destroy(gameObject);
                Destroy(transform.parent.gameObject);
            }
            else
            {
                mesh.enabled = false;
                collider.enabled = false;
            }
        }
    }
}
