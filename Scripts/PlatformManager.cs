using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject Player;
    private float startPosition;
    private float platformMovementInversionTimer;

    public int movementAmount;
    public int timeToMove;
    public int moveID;

    private bool invertMovement;

    public void Start()
    {
        startPosition = transform.position.y;
    }

    // when the player collides, set them as a child of the parent platform.
    public void OnTriggerEnter(Collider other)
    {
        // If the player collides with the transform, make it a child of the object.
        if(other.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }

    // when the player exits the collision, remove them as a child of the parent platform.
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = null;
        }
    }

    private void Update()
    {

        movePlatforms();
    }

    public void movePlatforms()
    {
        // store the time for the moving platforms.
        platformMovementInversionTimer += Time.deltaTime;

        // if enough time had passed, change from moving up to moving down.
        if (platformMovementInversionTimer < timeToMove)
        {
            invertMovement = true;
        }
        else
        {
            invertMovement = false;

            if (platformMovementInversionTimer > timeToMove * 2)
            {
                platformMovementInversionTimer = 0;
            }
        }

        int yMovementID = 1;
        int xMovementID = 2;

        // choose between platform moving up and down, left and right, and forward and backward.
        if (moveID == yMovementID)
        {
            // move platform up
            if (invertMovement)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - movementAmount * Time.deltaTime, transform.position.z);
            }
            else if (!invertMovement) // move platform down
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + movementAmount * Time.deltaTime, transform.position.z);
            }
        }
        else if (moveID == xMovementID)
        {
            // move platform left
            if (invertMovement)
            {
                transform.position = new Vector3(transform.position.x - movementAmount * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else if (!invertMovement) // move platform right
            {
                transform.position = new Vector3(transform.position.x + movementAmount * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }
        else
        {
            if (invertMovement) // move platform forward
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - movementAmount * Time.deltaTime);
            }
            else if (!invertMovement) // move platform backward
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + movementAmount * Time.deltaTime);
            }
        }
    }

}
