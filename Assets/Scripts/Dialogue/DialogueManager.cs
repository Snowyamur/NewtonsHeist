using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Text name_Text;
    public Text dialogue_Text;
    public Animator text_ani;
    public PlayerControlMapping control;
    public bool StartedDialogue;
    public bool progressText;
    public GameObject pane;
    public int line_counter;
    public int index_counter;
    public int[] indexes_of_characters;
    public string[] array_of_names;
    public string[] s_array;
    public GameObject[] character_images;

    //Creates a new queue for text to appear
    void Start()
    {
        line_counter = index_counter = 0;
        pane = name_Text.transform.parent.gameObject;
        Debug.Log("Started");
        StartedDialogue = false;
        progressText = false;
        sentences = new Queue<string>();
    }

    private void FixedUpdate()
    {
        if (progressText && Input.anyKey)
        {
            display_char_art(name_Text.text);
            StopAllCoroutines();
            control.ToggleInput(20f);
            DisplayNext();
            progressText = false;
            
        }

    }
    // Starts the dialogue
    public void StartDialogue(Dialogue dialogue, int[] index, string[] names, string[] s)
    {

        pane.SetActive(true);
        StartedDialogue = true;
        //Time.timeScale = 0f;
        control.NoInput();

        array_of_names = names;
        indexes_of_characters = index;
        s_array = s;

        // animator for showing the text box
        //text_ani.SetBool("Opened", true);
        //Debug.Log("Starting.............." + dialogue.char_name);
        name_Text.text = dialogue.char_name;
        
        // Clears queue if anything inside
        sentences.Clear();
        // Enqueue each sentence available
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
        DisplayNext();
        

    }
    public void DisplayNext()
    {
        //Debug.Log("called");
        if (line_counter < s_array.Length)
        {
            //Debug.Log(line_counter.ToString() + " " + indexes_of_characters[line_counter].ToString());
            if (line_counter == indexes_of_characters[index_counter])
            {
                name_Text.text = array_of_names[index_counter];
                index_counter++;

            }
            line_counter++;
        }
        display_char_art(name_Text.text);
        // Ends dialogue if no more text
        if (sentences.Count == 0)
        {
            EndChat();
            return;
        }
        
        // Dequeue text one by one
        string sentence = sentences.Dequeue();
        //Debug.Log(sentence);

        // Stops all coroutine if any
        StopAllCoroutines();
        // Progresses text one letter at a time
        StartCoroutine(Progress_Text(sentence));

    }

    // Coroutine for progressing text one letter at a time
    IEnumerator Progress_Text(string sentence)
    {
        dialogue_Text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogue_Text.text += letter;
            yield return null;
        }
        progressText = true;

    }


    // End chat for animator to disappear dialogue box

    public void EndChat()
    {
        pane.SetActive(false);
        //Debug.Log("Ending...");
        //text_ani.SetBool("Opened", false);
        //Time.timeScale = 1f;
        control.StartInput();
        StartedDialogue = false;
        index_counter = line_counter = 0;
        foreach (GameObject g in character_images)
        {
            g.SetActive(false);
        }
    }

    public void display_char_art(string s)
    {
        foreach (GameObject g in character_images)
        {
            g.GetComponent<Image>().color = Color.gray;
        }
        if (s == "Kep")
        {
            character_images[0].SetActive(true);
            character_images[0].GetComponent<Image>().color = Color.white;
        }
        else if (s == "Rob")
        {
            character_images[1].SetActive(true);
            character_images[1].GetComponent<Image>().color = Color.white;
        }
        else if (s == "3rd Guy")
        {
            character_images[2].SetActive(true);
            character_images[2].GetComponent<Image>().color = Color.white;
        }
    }

}
