using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNext : MonoBehaviour {

    private IEnumerator coRoutine;
    public float timeToWait = 0.8f;

    // when the player collides with the pickup
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            coRoutine = waitbeforeNextLevel(timeToWait);
            StartCoroutine(coRoutine);
        }
    }
    // wait an amount of seconds before going to the next level
    private IEnumerator waitbeforeNextLevel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // load the next level in the build order.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
