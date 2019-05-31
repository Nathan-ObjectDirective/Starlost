using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool goToControls;
    public bool goToMain;
    public bool goToLevel1;

    public void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            playGame();
        }

        if (goToControls)
        {
            if (Input.GetButtonDown("Cancel") || Input.GetKeyDown("escape"))
            {
                Application.Quit();
            }
        }

                     
    }

    public void playGame()
    {
        if (goToControls)
        {
            SceneManager.LoadScene("StoryControls");
        }
        else if(goToLevel1)
        {
            SceneManager.LoadScene("Level1");
        }
        else if(goToMain)
        {
            SceneManager.LoadScene("Menu");
        }
    }

  

}
