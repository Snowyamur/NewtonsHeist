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

    //Creates a new queue for text to appear
    void Start()
    {
        Debug.Log("Started");
        StartedDialogue = false;
        progressText = false;
        sentences = new Queue<string>();
    }

    private void FixedUpdate()
    {
        if (progressText && Input.anyKey)
        {
            StopAllCoroutines();
            control.ToggleInput(20f);
            DisplayNext();
            progressText = false;
        }

    }
    // Starts the dialogue
    public void StartDialogue(Dialogue dialogue)
    {
        StartedDialogue = true;
        //Time.timeScale = 0f;
        control.NoInput();
        // animator for showing the text box
        text_ani.SetBool("Opened", true);
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
        //Debug.Log("Ending...");
        text_ani.SetBool("Opened", false);
        //Time.timeScale = 1f;
        control.StartInput();
        StartedDialogue = false;
    }

}
