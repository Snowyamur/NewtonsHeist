using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

  //Need to track current menu,
  //changing sprites for non-current menu and closing other menus when a new one is pressed

    /*public enum Menu
    {
		    MainMenu,
		    NewGame,
		    Continue,
        Options,
        Extra,
        Quit
	}*/

	//public Menu currentMenu;

  float volume = 1.0f;
  string screenSize;
  int posRes = 0;

  Resolution[] resolutions;

  public Text currentRes;

  Button lowerRes;
  Button higherRes;
  Button applyBut;

  void Start()
  {
      resolutions = Screen.resolutions;
      currentRes.text = ResToString(Screen.currentResolution);
      applyBut = GameObject.Find("Apply").GetComponent<Button>();
      lowerRes = GameObject.Find("LowText").GetComponent<Button>();
      higherRes = GameObject.Find("HighText").GetComponent<Button>();
      lowerRes.onClick.AddListener(LowerResolution);
      higherRes.onClick.AddListener(UpResolution);
      applyBut.onClick.AddListener(Apply);
  }

  string ResToString(Resolution res)
  {
      return res.width + " x " + res.height;
  }

  void UpResolution()
  {
      if(posRes < resolutions.Length-1)
      {
        posRes++;
      }
      else
      {
          posRes = 0;
      }

      currentRes.text = ResToString(resolutions[posRes]);
  }

  void LowerResolution()
  {
      if(posRes > 0)
      {
        posRes--;
      }
      else
      {
          posRes = resolutions.Length-1;
      }

      currentRes.text = ResToString(resolutions[posRes]);
  }

  void Apply()
  {
      Screen.SetResolution(resolutions[posRes].width, resolutions[posRes].height, true);
  }



  /*void OnGUI () //Old UI, un-comment for testing
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

            if(GUILayout.Button("Extra"))
            {
				SaveLoad.Load();
				currentMenu = Menu.Extra;
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
        volume = GUILayout.HorizontalSlider(volume, 0.0f, 1.0f);
        AudioListener.volume = volume;
        GUILayout.Space(10);

        if(Screen.fullScreen) //Change text of button based on current display
        {
            screenSize = "Fullscreen";
        }
        else
        {
            screenSize = "Windowed";
        }

        if(GUILayout.Button(screenSize)) //Toggles display between fullscreen and windowed
        {
    				Screen.fullScreen = !Screen.fullScreen;
            if(Screen.fullScreen)
            {
                screenSize = "Fullscreen";
            }
            else
            {
                screenSize = "Windowed";
            }
  		}

        if(GUILayout.Button("Back")) //Back to go back to Main Menu
        {
    		currentMenu = Menu.MainMenu;
  		}

    }

    else if(currentMenu == Menu.Extra)
    {
        //TODO
    }

		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();

	}*/

}
