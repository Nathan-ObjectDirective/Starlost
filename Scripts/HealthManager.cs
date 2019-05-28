using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHP;
    public int currentHP;

    public float invincibilityLength;
    public float invincibilityTimer;
    public float flashLength = 0.1f;
    private float flashTimer;

    public Text healthText;
    public GameObject player;
    private PlayerController playerController;
    private Vector3 respawnLocation;
    private Renderer playerRenderer;



    // Use this for initialization
    void Start ()
    {
        currentHP = maxHP;

        // store the player and renderer.
        playerRenderer = player.GetComponent<MeshRenderer>();
        playerController = FindObjectOfType<PlayerController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        checkInvincibility();

        // if the player falls far enough, respawn him.
        if (player.transform.position.y < 10)
        {
            respawnPlayer();

            int noDamage = 0;
            Vector3 noDirection = new Vector3();

            damagePlayer(noDamage, noDirection);
        }

        healthText.text = "Health: " + currentHP;
    }

    // do damage to the player
    public void damagePlayer(int damage, Vector3 direction)
    {
        if (invincibilityTimer <= 0)
        {
            // take away from his health
            currentHP -= damage;

            // if the player loses all health, respawn.
            if(currentHP <= 0)
            {
                respawnPlayer();
            }

            // knockback the player based on a direction.
            playerController.knockBack(direction);

            invincibilityTimer = invincibilityLength;

            playerRenderer.enabled = false;
            flashTimer = flashLength;
        }
    }

    // respawn the player
    public void respawnPlayer()
    {
        // teleport player to respawn location
        player.transform.position = this.respawnLocation;

        // refresh hp to max
        currentHP = maxHP;
    }

    // set a respawn point for the player.
    public void setRespawnLocation(Vector3 respawnPoint)
    {
        this.respawnLocation = respawnPoint;
    }

    public void checkInvincibility()
    {
        // if we are still invincible 
        if (invincibilityTimer > 0)
        {
            // reduce invincibility timer
            invincibilityTimer -= Time.deltaTime;

            // reduce flash timer
            flashTimer -= Time.deltaTime;


            if (flashTimer <= 0)
            {
                // reverse the renderering ability
                playerRenderer.enabled = !playerRenderer.enabled;
                flashTimer = flashLength;
            }

            if (invincibilityTimer <= 0)
            {
                playerRenderer.enabled = true;
            }
        }
    }
}
