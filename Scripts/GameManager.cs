using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int currentAvailablePlatformsToSpawn;
    private int pickUpHeight = 0;

    private float pickUpRespawnTimer;
    private float levelTimer;

    public bool isDisableHint;
    public bool isLastLevel;
    private bool platformSpawned = false;

    public Text platformAvailableText;
    public Text timeText;
    public Text hintText;

    public GameObject firstPlatform;
    public GameObject player;
    public GameObject pickup;

    private Vector3 startingPickUpPositionOne;
    private Vector3 startingPickUpPositionTwo;
 
    // Use this for initialization
    void Start ()
    {
        // hide the mouse cursor
        Cursor.visible = false;

        // if disable hint is checked
        if (isDisableHint)
        {
            disableHint();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {

        checkDeathPlane();

        checkPickUpsRespawn();

        updateText();
    }

    //  increment the amount of platforms the player can spawn
    public void addAvailablePlatforms(int pointAmount)
    {
        currentAvailablePlatformsToSpawn += pointAmount;

        // get and play a pickup sound
        AudioSource[] audio;
        audio = GetComponents<AudioSource>();
        audio[1].Play();
    }

    // decrement the amount of platforms the player can spawn
    public void removeAvailablePlatforms(int pointAmount)
    {
        currentAvailablePlatformsToSpawn -= pointAmount;
    }

    public int getPoints()
    {
        return currentAvailablePlatformsToSpawn;
    }

    // spawn the first platform as the player falls
    public void spawnFirstPlatform()
    {
        // calc the platform position to spawn appropriately with player.
        Vector3 platformPosition = new Vector3(player.transform.position.x, player.transform.position.y-10, player.transform.position.z);

        // store the inital pickup positions;
        float seperationValue = 2;
        startingPickUpPositionOne = new Vector3(platformPosition.x + seperationValue, platformPosition.y + 1, platformPosition.z + seperationValue);
        startingPickUpPositionTwo = new Vector3(platformPosition.x - seperationValue, platformPosition.y + 1, platformPosition.z - seperationValue);

        Quaternion noRotation = Quaternion.Euler(0, 0, 0);

        // spawn the platforms and pickups.
        Instantiate(firstPlatform, platformPosition, noRotation);
        Instantiate(pickup, startingPickUpPositionOne, noRotation);
        Instantiate(pickup, startingPickUpPositionTwo, noRotation);


        // Spawn some more if we aren't on the last level.
        if (!isLastLevel)
        {
            startingPickUpPositionOne.y += 1;
            startingPickUpPositionTwo.y += 1;

            Instantiate(pickup, startingPickUpPositionOne, noRotation);
            Instantiate(pickup, startingPickUpPositionTwo, noRotation);
        }
    }

    // respawn the pickups at the start of the level
    public void respawnPickUps()
    {
        // spawn the pickups going up and down
        if (pickUpHeight < 3)
        {
            startingPickUpPositionOne.y += 1;
            startingPickUpPositionTwo.y += 1;
        }
        else if(pickUpHeight > 3)
        {
            pickUpHeight = 0;

            startingPickUpPositionOne.y -= 1;
            startingPickUpPositionTwo.y -= 1;
        }
        else if(pickUpHeight > 6)
        {
            pickUpHeight = 0;
        }
   
        pickUpHeight++;

        // spawn both pickups
        Instantiate(pickup, startingPickUpPositionOne, Quaternion.Euler(0, 0, 0));
        Instantiate(pickup, startingPickUpPositionTwo, Quaternion.Euler(0, 0, 0));


    }

    public void disableHint()
    {
        hintText.enabled = false;
    }

    public void checkDeathPlane()
    {
        // if the player has fallen long enough
        if (player.transform.position.y < 30 && !platformSpawned)
        {
            spawnFirstPlatform();

            platformSpawned = true;

            FindObjectOfType<HealthManager>().setRespawnLocation(player.transform.position);
        }
    }

    public void checkPickUpsRespawn()
    {
        pickUpRespawnTimer += Time.deltaTime;
        levelTimer += Time.deltaTime;

        if (pickUpRespawnTimer > 15)
        {
            respawnPickUps();

            pickUpRespawnTimer = 0;
        }
    }

    public void updateText()
    {
        // convert seconds to minutes and seconds 
        int min = Mathf.FloorToInt(levelTimer / 60);
        int sec = Mathf.FloorToInt(levelTimer % 60);

        timeText.text = "Time elapsed: " + min.ToString("00") + ":" + sec.ToString("00");

        platformAvailableText.text = "Bounce platforms available: " + currentAvailablePlatformsToSpawn;
    }
}
