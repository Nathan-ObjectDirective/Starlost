using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float r = 0.5f;
    public float speed = 0.01f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Vector3 moveDelta = new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);

        //transform.Translate(moveDelta, Space.World);

        //Vector3 rotationAxis = Vector3.Cross(moveDelta.normalized, Vector3.forward);

        //transform.RotateAround(transform.position, rotationAxis, Mathf.Sin(moveDelta.magnitude * r * 2 * Mathf.PI) * Mathf.Rad2Deg);
        transform.Rotate(Input.GetAxis("Vertical")* speed, 0, (Input.GetAxis("Horizontal")*-1)* speed, Space.World);
    }
    }
