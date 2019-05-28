using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int bouncyPlatformsAvailable;

    bool canBounce = false;

    public float movementSpeed;
    public float jumpHeight;
    public float knockBackAmount;
    public float knockBackLength;
    private float knockBackTimer;
    public float gravityScaler;

    private CharacterController playerCharacterController;
    private Vector3 directionToMove;
    public GameObject bouncyPlatform;

    // Use this for initialization
    void Start ()
    {
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
            // spawn the bouncy platform
            Instantiate(bouncyPlatform, playerPosition, Quaternion.Euler(0, 0, 0));

            FindObjectOfType<GameManager>().removeAvailablePlatforms(1);

            // disable the hint after they spawn their first platform
            FindObjectOfType<GameManager>().disableHint();
        }
    }

    // Update is called once per frame
    void LateUpdate ()
    {
        // if we aren't being knocked back
        if (knockBackTimer <= 0)
        {
            // store the y movement value before it is altered by input
            float yStore = directionToMove.y;

            // base the move direction on the axis of the right and left analog sticks.
            directionToMove = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));

            // noramlise the magnitude of the move direction
            if (directionToMove.magnitude > 1)
            {
                directionToMove = directionToMove.normalized * movementSpeed;
            }
            else
            {
                directionToMove = directionToMove * movementSpeed;
            }

            // reinstate the y movement value
            directionToMove.y = yStore;

            // if the player is on the ground
            if (playerCharacterController.isGrounded)
            {
                // stop gravity pushing down for now
                directionToMove.y = 0f;

                // if the player has hit a bounce platform
                if (canBounce)
                {
                    // launch the player far up into the air.
                    directionToMove.y = (jumpHeight * 3)-15;

                    // play the bounce sound
                    AudioSource audio = GetComponent<AudioSource>();
                    audio.Play();
                    canBounce = false;
                }

                // allow the player to jump
                if (Input.GetButtonDown("RightBumper") || Input.GetButtonDown("Jump"))
                {
                    directionToMove.y = jumpHeight;

                    AudioSource audio = GetComponent<AudioSource>();

                    // play the jump sound
                    audio.Play();
                }
            }
        }
        else
        {
            knockBackTimer -= Time.deltaTime;
        }


        // Apply gravity and gravity scale
        directionToMove.y += (Physics.gravity.y * gravityScaler * Time.deltaTime);

        // move the charatcer controller in a move dire
        playerCharacterController.Move(directionToMove * Time.deltaTime);
        
    }

    // knockback the player
    public void knockBack(Vector3 direction)
    {
        // set the knockback timer
        knockBackTimer = knockBackLength;

        // alter the movement direction by the direction passed in
        directionToMove = direction * knockBackAmount;

        // knock the player into the air slightly
        directionToMove.y = knockBackAmount;
    }

    // allow the player to bounce
    public void bounce()
    {
        canBounce = true;
    }
}
