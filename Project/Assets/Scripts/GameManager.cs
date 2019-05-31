using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int currentPoints;
    public Text pointText;
 
    public Text timeText;
    public Text hintText;

    public GameObject firstPlatform;
    public GameObject player;
    public GameObject pickup;
    private bool platformSpawned = false;

    private Vector3 pickupPosition1;
    private Vector3 pickupPosition2;
    public bool isDisableHint;
    public bool isLastLevel;

    private int count = 0;

    float timePassed;

    float time;

    // Use this for initialization
    void Start ()
    {
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
        // if the player has fallen long enough
		if(player.transform.position.y < 30 && !platformSpawned)
        {
            spawnFirstPlatform();

            platformSpawned = true;
          
            FindObjectOfType<HealthManager>().setRespawnPoint(player.transform.position);
        }

        // if enough time has passed, respawn the pickups.
        timePassed += Time.deltaTime;
        time += Time.deltaTime;
        if (timePassed > 15)
        {
            respawnPickUps();

            timePassed = 0;
        }

        // convert seconds to minutes and seconds 
        int min = Mathf.FloorToInt(time / 60);
        int sec = Mathf.FloorToInt(time % 60);
   
        timeText.text = "Time elapsed: " + min.ToString("00") + ":" + sec.ToString("00");

        pointText.text = "Bounce platforms available: " + currentPoints;
    }

    // add points for the player to use to spawn more platforms
    public void addPoints(int pointAmount)
    {
        currentPoints += pointAmount;

        // get and play the audio
        AudioSource[] audio;
        audio = GetComponents<AudioSource>();
        audio[1].Play();
    }

    public void removePoints(int pointAmount)
    {
        currentPoints -= pointAmount;
    }

    public int getPoints()
    {
        return currentPoints;
    }

    // spawn the first platform as the player falls
    public void spawnFirstPlatform()
    {
        // calc the platform position to spawn appropriately
        Vector3 platformPosition = new Vector3(player.transform.position.x, player.transform.position.y-10, player.transform.position.z);

        // store the inital pickup position
        pickupPosition1 = new Vector3(platformPosition.x+2, platformPosition.y + 1, platformPosition.z + 2);
        pickupPosition2 = new Vector3(platformPosition.x -2, platformPosition.y + 1, platformPosition.z-2);

        Instantiate(firstPlatform, platformPosition, Quaternion.Euler(0, 0, 0));

        Instantiate(pickup, pickupPosition1, Quaternion.Euler(0, 0, 0));
        Instantiate(pickup, pickupPosition2, Quaternion.Euler(0, 0, 0));

        pickupPosition1.y += 1;
        pickupPosition2.y += 1;

        if (!isLastLevel)
        {

            Instantiate(pickup, pickupPosition1, Quaternion.Euler(0, 0, 0));
            Instantiate(pickup, pickupPosition2, Quaternion.Euler(0, 0, 0));
        }
    }

    // respawn the pickups at the start of the level
    public void respawnPickUps()
    {
        // spawn the pickups going up and down
        if (count < 3)
        {
            pickupPosition1.y += 1;
            pickupPosition2.y += 1;
        }
        else if(count > 3)
        {
            count = 0;

            pickupPosition1.y -= 1;
            pickupPosition2.y -= 1;
        }
        else if(count > 6)
        {
            count = 0;
        }
   
        count++;

        // spawn both pickups
        Instantiate(pickup, pickupPosition1, Quaternion.Euler(0, 0, 0));
        Instantiate(pickup, pickupPosition2, Quaternion.Euler(0, 0, 0));


    }

    public void disableHint()
    {
        hintText.enabled = false;
    }
}
