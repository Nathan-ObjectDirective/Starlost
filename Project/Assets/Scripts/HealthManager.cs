using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHP;
    public int currentHP;

    public PlayerController thePlayer;
    public float invincibilityLength;
    public float invincibilityCounter;

    public GameObject player;
    private Renderer playerRenderer;
    private GameObject model;
    private float flashCounter;
    public float flashLength = 0.1f;

    private bool isRespawning;
    private Vector3 respawnPoint;

    public Text healthText;

    // Use this for initialization
    void Start ()
    {
        currentHP = maxHP;

        // store the player and renderer.
        model = GameObject.FindGameObjectWithTag("Model");
        playerRenderer = player.GetComponent<MeshRenderer>();
        thePlayer = FindObjectOfType<PlayerController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // if we are still invincible 
		if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;


            if(flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                model.SetActive(playerRenderer.enabled);
                flashCounter = flashLength;
            }

            if(invincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
                model.SetActive(true);
            }
        }

        // if the player falls far enough, respawn him.
        if(player.transform.position.y < 10)
        {
            respawn();
            damagePlayer(0, new Vector3(0,0,0) );
        }

        healthText.text = "Health: " + currentHP;
    }

    // do damage to the player
    public void damagePlayer(int damage, Vector3 direction)
    {
        if (invincibilityCounter <= 0)
        {
            // take away from his health
            currentHP -= damage;

            // if the player loses all health, respawn.
            if(currentHP <= 0)
            {
                respawn();
            }

            // knockback the player based on a direction.
            thePlayer.knockBack(direction);

            invincibilityCounter = invincibilityLength;

            playerRenderer.enabled = false;
            model.SetActive(false);
            flashCounter = flashLength;
        }
    }

    public void healPlayer(int heal)
    {
        currentHP += heal;

        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    // respawn the player
    public void respawn()
    {
        player.transform.position = this.respawnPoint;
        currentHP = maxHP;
    }

    // set a respawn point for the player.
    public void setRespawnPoint(Vector3 respawnPoint)
    {
        this.respawnPoint = respawnPoint;
    }
}
