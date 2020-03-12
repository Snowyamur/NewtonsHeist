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
  bool showGUI = false;

  Resolution[] resolutions;

  Text currentRes;
  Text saveFile;

  Button newGame;
  Button play;
  Button lowerRes;
  Button higherRes;
  Button apply;

  Slider volumeSlider;

  GameObject continueMenu;

  GameObject canvas;
  GameObject optionsMenu;

  void Start()
  {
      canvas = GameObject.Find("Canvas");

      //Options Stuff
      optionsMenu = canvas.transform.Find("OptionsMenu").gameObject;
      volumeSlider = optionsMenu.transform.Find("VolumeSlider").gameObject.GetComponent<Slider>();
      currentRes = optionsMenu.transform.Find("CurrentRes").gameObject.GetComponent<Text>();
      resolutions = Screen.resolutions;
      currentRes.text = ResToString(Screen.currentResolution);

      lowerRes = optionsMenu.transform.Find("LowText").gameObject.GetComponent<Button>();
      lowerRes.onClick.AddListener(LowerResolution);

      higherRes = optionsMenu.transform.Find("HighText").gameObject.GetComponent<Button>();
      higherRes.onClick.AddListener(UpResolution);

      apply = optionsMenu.transform.Find("Apply").gameObject.GetComponent<Button>();
      apply.onClick.AddListener(Apply);

      //Continue Stuff
      continueMenu = canvas.transform.Find("ContinueMenu").gameObject;

      //saveFile = GameObject.Find("Save File").GetComponent<Text>();

      /*play = GameObject.Find("Play").GetComponent<Button>();
      play.onClick.AddListener(Play);*/

      //New Game Stuff
      newGame = GameObject.Find("New Game").GetComponent<Button>();
      newGame.onClick.AddListener(StartGame);

      //Clean-up
      optionsMenu.SetActive(false);
  }

  public void StartGame()
  {
      LevelManager.current = new LevelManager(); //Create new Level Manager when starting new game
      //Save the current Game as a new saved Game
			SaveLoad.Save();
			//Move on to game...
			SceneManager.LoadScene(1);
  }

  public void ContinueGame()
  {
    if(SaveLoad.savedGames.Count != 0)
    {
        Play();
    }
    else //Disable continue menu if no save games are present
    {
        continueMenu = GameObject.Find("ContinueMenu");
        continueMenu.SetActive(false);
    }
    /*float i = 0f;
    foreach(LevelManager l in SaveLoad.savedGames)
    {
      //GameObject newButton = Instantiate(button) as GameObject;
      //newButton.transform.parent = continueMenu;
      CreateButton(continueMenu.transform, new Vector3(0f, 100f+i, 0f),
      new Vector2(1, 1), LoadScene, "l.playerData.sceneID");

      if(GUILayout.Button("Level: " + l.playerData.sceneID)) //Displays current level in save file
      {
        LevelManager.current = l; //Load saved LevelManager
        //Move on to game...
        SceneManager.LoadScene(LevelManager.current.playerData.sceneID); //Load last scene
      }
      i += 5;
    }*/
  }

  public void Play()
  {
    if(SaveLoad.savedGames.Count > 0)
    {
      LevelManager.current = SaveLoad.savedGames[SaveLoad.savedGames.Count-1];
      SceneManager.LoadScene(LevelManager.current.playerData.sceneID); //Load last scene
    }
  }

  void CreateButton(Transform parent, Vector3 position, Vector2 size,
  UnityEngine.Events.UnityAction method, string butText)
  {
    GameObject button = new GameObject();
    button.transform.parent = parent;
    button.AddComponent<RectTransform>();
    button.AddComponent<Button>();
    //button.GetComponent<Button>().text = "Level: " + butText;
    button.transform.position = position;
    //button.GetComponent<RectTransform>().SetSize(size);
    button.GetComponent<Button>().onClick.AddListener(method);
  }

  public void ChangeVolume()
  {
      AudioListener.volume = volumeSlider.value;
  }

  string ResToString(Resolution res)
  {
      return res.width + " x " + res.height;
  }

  public void UpResolution()
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

  public void LowerResolution()
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

  public void Apply()
  {
      Screen.SetResolution(resolutions[posRes].width, resolutions[posRes].height, true);
  }

  public void Quit()
  {
      Application.Quit();
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
