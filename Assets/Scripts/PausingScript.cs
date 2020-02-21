using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausingScript : MonoBehaviour
{
    private GameObject pauseMenu;
    private PlayerControlMapping control;
    private bool isCurrentlyPaused = false;

    void Awake()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.SetActive(false); // now that we found the pausemenu, make it inactive.
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
            PauseControl();
    }

    public void PauseGame()
    {
        isCurrentlyPaused = true;
        Time.timeScale = 0; //Stop time
        pauseMenu.SetActive(true); //Show pause menu
    }

    public void ResumeGame()
     {
        isCurrentlyPaused = false;
        Time.timeScale = 1; //Resume time
        pauseMenu.SetActive(false); //Hide pause menu
    }

    //Allows pausing game using external scripts
    public void PauseControl()
    {
        if (isCurrentlyPaused) //If pause menu is on
            ResumeGame();
        else //If pause menu is off
            PauseGame();
    }

    public void SaveGame()
    {

    }

    public void LoadGame()
    {

    }

    public void OpenOptions()
    {

    }

    public void MainMenu()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
