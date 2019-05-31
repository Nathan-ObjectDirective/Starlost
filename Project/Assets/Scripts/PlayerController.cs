using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private CharacterController playerCharacterController;
    private Vector3 moveDirection;
    public float gravityScale;
    int bouncyPlatformsAvailable;
    //private GameObject model;
    //private Rigidbody rbmodel;


    public GameObject bouncyPlatform;

    bool canBounce = false;

    public float knockBackForce;
    public float knockBackTime;
    public float knockBackCounter;

    // Use this for initialization
    void Start ()
    {
        //model = GameObject.FindGameObjectWithTag("Model");
        //rbmodel = model.GetComponent<Rigidbody>();
        playerCharacterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // get the amount of platforms available to spawn
        bouncyPlatformsAvailable = FindObjectOfType<GameManager>().getPoints();

        // store the player position to allow proper spawning the platform.
        Vector3 playerPosition = new Vector3(transform.position.x, transform.position.y-5, transform.position.z);


        // spawn a platform below the player
        if ((Input.GetButtonDown("LeftBumper") || Input.GetKeyDown("e")) && bouncyPlatformsAvailable > 0)
        {
            Instantiate(bouncyPlatform, playerPosition, Quaternion.Euler(0, 0, 0));

            FindObjectOfType<GameManager>().removePoints(1);

            FindObjectOfType<GameManager>().disableHint();
        }
    }

    // Update is called once per frame
    void LateUpdate ()
    {
        
        if (knockBackCounter <= 0)
        {

            float yStore = moveDirection.y;

            // base the move direction on the axis of the right and left analog sticks.
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));

            // noramlise the magnitude of the move direction
            if (moveDirection.magnitude > 1)
            {
                moveDirection = moveDirection.normalized * moveSpeed;
            }
            else
            {
                moveDirection = moveDirection * moveSpeed;
            }

            moveDirection.y = yStore;

            // if the player is on the ground
            if (playerCharacterController.isGrounded)
            {
                moveDirection.y = 0f;

                // if the player has hit a bounce platform
                if (canBounce)
                {
                    moveDirection.y = (jumpForce * 3)-15;

                    AudioSource audio = GetComponent<AudioSource>();
                    audio.Play();
                    canBounce = false;
                }

                // allow the player to jump
                if (Input.GetButtonDown("RightBumper") || Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;

                    AudioSource audio = GetComponent<AudioSource>();

                    // play the jump sound
                    audio.Play();
                }
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }

        
        // Apply gravity and gravity scale
        moveDirection.y += (Physics.gravity.y * gravityScale * Time.deltaTime);

        // move the charatcer controller in a move dire
        playerCharacterController.Move(moveDirection * Time.deltaTime);
        //Vector3 moveToPos = new Vector3(playerCharacterController.transform.position.x, playerCharacterController.transform.position.y, playerCharacterController.transform.position.z-0.73f);
        //Vector3 moveTo = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        //model.transform.position = moveDirection;
        //rbmodel.AddTorque(moveTo);

    }

    // knockback the player
    public void knockBack(Vector3 direction)
    {
        knockBackCounter = knockBackTime;

        moveDirection = direction * knockBackForce;

        moveDirection.y = knockBackForce;
    }

    // allow the player to bounce
    public void bounce()
    {
        canBounce = true;
    }
}
