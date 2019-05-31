using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacher : MonoBehaviour
{
    public GameObject Player;
    private float startPosition;
    private float updates;

    public int movementAmount;
    public int timeToMove;
    public int moveID;

    public void Start()
    {
        startPosition = transform.position.y;
    }

    // when the player collides, set them as a child of the parent platform.
    public void OnTriggerEnter(Collider other)
    {
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

    private bool movingUp;
    private bool movingDown;

    private void Update()
    {
        // store the time for the moving platforms.
        updates += Time.deltaTime;

        // if enough time had passed, change from moving up to moving down.
        if(updates < timeToMove)
        {
            movingUp = true;
        }
        else
        {
            movingUp = false;

            if (updates > timeToMove*2)
            {
                updates = 0;
            }
        }


        // choose between platform moving up and down, left and right, and forward and backward.
        if (moveID == 1)
        {
            // move platform up
            if (movingUp)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - movementAmount * Time.deltaTime, transform.position.z);
            }
            else if (!movingUp) // move platform down
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + movementAmount * Time.deltaTime, transform.position.z);
            }
        }
        else if(moveID == 2)
        {
            // move platform left
            if (movingUp)
            {
                transform.position = new Vector3(transform.position.x - movementAmount * Time.deltaTime, transform.position.y , transform.position.z);
            }
            else if (!movingUp) // move platform right
            {
                transform.position = new Vector3(transform.position.x + movementAmount * Time.deltaTime, transform.position.y , transform.position.z);
            }
        }
        else
        {
            if (movingUp) // move platform forward
            {
                transform.position = new Vector3(transform.position.x, transform.position.y , transform.position.z - movementAmount * Time.deltaTime);
            }
            else if (!movingUp) // move platform backward
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + movementAmount * Time.deltaTime);
            }
        }


}

}
