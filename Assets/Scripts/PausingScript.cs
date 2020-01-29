using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausingScript : MonoBehaviour
{
    GameObject pauseMenu;

    PlayerControlMapping control;

    void Awake()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
    }

    void Start()
    {
        Time.timeScale = 1;
        control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControlMapping>();
    }

    void Update()
    {
        //Toggle pause menu when pressing Escape
        if(control.pause)
        {
            if(Time.timeScale == 0) //If pause menu is on
            {
                Time.timeScale = 1; //Resume time
                ResumeGame();
            }
            else if(Time.timeScale == 1) //If pause menu is off
            {
                Time.timeScale = 0; //Stop time
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true); //Show pause menu
    }

    public void ResumeGame()
     {
        pauseMenu.SetActive(false); //Hide pause menu
    }

    //Allows pausing game using external scripts
    public void PauseControl()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
            ResumeGame();
        }
        else if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            PauseGame();
        }
    }
}
