using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    private IEnumerator coroutine;
    public float timeToWait = 0.8f;
    public bool isLastLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            coroutine = waitBeforeNextDestination(0.8f);
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator waitBeforeNextDestination(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // If we are on the last level
        if (isLastLevel)
        {
            SceneManager.LoadScene("Ending");
        }
        else // go to the main menu
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
