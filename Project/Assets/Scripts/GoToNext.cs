using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNext : MonoBehaviour {

    private IEnumerator coroutine;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // when the player collides with the pickup
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            coroutine = WaitAndPrint(0.8f);
            StartCoroutine(coroutine);
        }
    }
    // wait an amount of seconds before going to the next level
    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
