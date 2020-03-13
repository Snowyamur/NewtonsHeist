using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausingScript : MonoBehaviour
{
    private GameObject pauseMenu;
    private Button loadButton;

    private GameObject player;
    private PlayerMechanics playerMechanics;
    private PlayerStats stats;
    private PlayerControlMapping control;
    private bool isCurrentlyPaused = false;

    void Awake()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.SetActive(false); // now that we found the pausemenu, make it inactive.

        loadButton = pauseMenu.transform.GetChild(2).GetComponent<Button>();
        if (SaveLoad.savedGames.Count != 0)
        {
            loadButton.interactable = false;
        }

        player = GameObject.FindGameObjectWithTag("Player");
        playerMechanics = player.GetComponent<PlayerMechanics>();
        stats = LevelManager.current.playerData;
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
        SaveData();
        SaveLoad.Save();
        loadButton.interactable = true;
    }

    

    public void LoadGame()
    {
        if (SaveLoad.savedGames.Count > 0)
        {
            LevelManager.current = SaveLoad.savedGames[SaveLoad.savedGames.Count - 1];

            LevelManager.current.isSceneBeingLoaded = true;
            Debug.Log(LevelManager.current.isSceneBeingLoaded);
            SceneManager.LoadScene(LevelManager.current.playerData.sceneID); // Load last scene
        }
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


    // ----- HELPER FUNCTIONS ----- 
    private void SaveData()
    {
        stats.sceneID = SceneManager.GetActiveScene().buildIndex;

        stats.playerPosX = player.transform.position.x;
        stats.playerPosY = player.transform.position.y;
        stats.playerPosZ = player.transform.position.z;

        stats.powers = playerMechanics.GetPowers();
        stats.grenades = playerMechanics.GetGrenades();

        stats.currentGrenade = playerMechanics.GetCurrentGrenade();
    }
}
