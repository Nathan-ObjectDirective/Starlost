using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickup : MonoBehaviour
{
    public int pointValue = 1;
    public GameObject pickupEffect;
    Renderer mesh;
    BoxCollider collid;

    public bool isGoal;

	// Use this for initialization
	void Start ()
    {
        mesh = GetComponent<MeshRenderer>();
        collid = GetComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            FindObjectOfType<GameManager>().addPoints(1);

            Instantiate(pickupEffect, transform.position, transform.rotation);
            
            if (!isGoal)
            {
                Destroy(gameObject);
                Destroy(transform.parent.gameObject);
            }
            else
            {
                mesh.enabled = false;
                collid.enabled = false;
            }
        }
    }
}
