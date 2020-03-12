using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScenes : MonoBehaviour
{
    float fps = 3f;
    float delay = 4f;
    float radius = 1f;
    float xPos;
    float yPos;
    int levelIndex;
    int newScene;

    Fade fade;
    PlayerControlMapping control;
    //GameObject blackScreen;
    Image blackScreen;
    GameObject player;

    void Awake()
    {
        newScene = GameObject.FindGameObjectWithTag("Canvas").GetComponent<SceneData>().getNextScene();
        xPos = GameObject.FindGameObjectWithTag("Canvas").GetComponent<SceneData>().xPos;
        yPos = GameObject.FindGameObjectWithTag("Canvas").GetComponent<SceneData>().yPos;
        player = GameObject.FindGameObjectWithTag("Player");
        control = player.GetComponent<PlayerControlMapping>();
        blackScreen = GameObject.FindGameObjectWithTag("BlackScreen").GetComponent<Image>();
        fade = blackScreen.GetComponent<Fade>();

        blackScreen.color = new Color(0f, 0f, 0f, 0f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.tag == "Player")
        {
            //Debug.Log(newScene.getScene());
            ChangeScene(newScene, xPos, yPos);
        }
    }

    public void ChangeScene(int scene, float xPos, float yPos)
    {
        StartCoroutine(control.ToggleInput(delay));
        StartCoroutine(fade.FadeImageInOut(fps, fps, blackScreen, delay*2)); //Begins fade to black
        player.transform.position = new Vector3(xPos, yPos, 0); //Changes player's position in new scene
        SaveLoad.Save(); //Save data
        SceneManager.LoadScene(scene); //Load new scene
    }
}
