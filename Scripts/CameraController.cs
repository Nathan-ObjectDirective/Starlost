using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public float rotateSpeed;

    public Transform pivot;
    public float maxViewAngle;
    public float minViewAngle;

    // Use this for initialization
    void Start ()
    {
        // set the camera offset based on the player
        offset = target.position - transform.position;

        // move the pivot to the target object
        pivot.transform.position = target.transform.position;

        // set the pivot as a child of the target
        pivot.transform.parent = target.transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // get the axis of the right trigger
        float horizontal = Input.GetAxis("RStickX") * rotateSpeed * Time.deltaTime;
        target.Rotate(0, horizontal, 0);

        // get the axis of the left trigger
        float vertical = Input.GetAxis("RStickY") * rotateSpeed * Time.deltaTime;
        pivot.Rotate(-vertical, 0, 0);

        // Lock between a max rotation to avoid camera clipping and jumping.
        if(pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        // Lock between a min rotation to avoid camera clipping and jumping.
        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }

        float yAngle = target.eulerAngles.y;
        float xAngle = pivot.eulerAngles.x;

        // set up the angle to rotate
        Quaternion rotation = Quaternion.Euler(xAngle, yAngle, 0);

        // move the camera position based on the position and rotation of the target object
        transform.position = target.position - (rotation * offset);

        // if the camera is going to go under or flip around the target
        if(transform.position.y < target.position.y)
        {
            // reset the camera position to prevent flipping the camera
            transform.position = new Vector3(transform.position.x, target.position.y-.5f, transform.position.z);
        }

        // rotate the camera to look at the target as it moves
        transform.LookAt(target);
	}
}
