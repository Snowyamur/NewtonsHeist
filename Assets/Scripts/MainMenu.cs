using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public enum Menu
    {
		MainMenu,
		NewGame,
		Continue,
    Options,
    Extras,
    Quit
	}

	public Menu currentMenu;

  float volume = 1.0f;

  void OnGUI ()
  {

		GUILayout.BeginArea(new Rect(0,0,Screen.width, Screen.height));
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();

		if(currentMenu == Menu.MainMenu)
    {

			GUILayout.Box("Newton's Heist");
			GUILayout.Space(10);

			if(GUILayout.Button("New Game"))
      {
				LevelManager.current = new LevelManager(); //Create new Level Manager when starting new game
				currentMenu = Menu.NewGame;
			}

			if(GUILayout.Button("Continue"))
      {
				currentMenu = Menu.Continue;
			}

      if(GUILayout.Button("Options"))
      {
				currentMenu = Menu.Options;
			}

      if(GUILayout.Button("Extras"))
      {
				SaveLoad.Load();
				currentMenu = Menu.Extras;
			}

			if(GUILayout.Button("Quit Game"))
      {
				Application.Quit();
			}
		}

		else if(currentMenu == Menu.NewGame) //IF New Game was chosen
    {
			//Save the current Game as a new saved Game
			SaveLoad.Save();
			//Move on to game...
			SceneManager.LoadScene(1);
		}

		else if(currentMenu == Menu.Continue) //If Continue was chosen
    {

			GUILayout.Box("Select Save File");
			GUILayout.Space(10);

			foreach(LevelManager l in SaveLoad.savedGames)
      {
				if(GUILayout.Button("Level: " + l.playerData.sceneID)) //Displays current level in save file
        {
					LevelManager.current = l; //Load saved LevelManager
					//Move on to game...
					SceneManager.LoadScene(LevelManager.current.playerData.sceneID); //Load last scene
				}

			}

			GUILayout.Space(10);
			if(GUILayout.Button("Cancel")) //Cancel to go back to Main Menu
      {
				currentMenu = Menu.MainMenu;
			}

		}

    else if(currentMenu == Menu.Options)
    {
        GUILayout.Box("Volume");
        GUILayout.Space(10);
        volume = GUILayout.HorizontalSlider(volume ,0.0f,1.0f);
        AudioListener.volume = volume;
        GUILayout.Space(10);


        if(GUILayout.Button("Back")) //Cancel to go back to Main Menu
        {
  				currentMenu = Menu.MainMenu;
  			}

    }

    else if(currentMenu == Menu.Extras)
    {
        //TODO
    }

		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();

	}

}
