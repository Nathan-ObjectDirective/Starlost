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

        pivot.transform.position = target.transform.position;

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

        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);

        transform.position = target.position - (rotation * offset);

        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y-.5f, transform.position.z);
        }

        transform.LookAt(target);
	}
}
