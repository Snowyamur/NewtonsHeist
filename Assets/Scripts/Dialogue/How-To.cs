using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowTo : MonoBehaviour
{
    // Insert Dialogue_Canvas into the hierarchy
    // Insert Dialogue_Manager into the hierarchy

    // under Dialogue_Canvas > DialogueBox
    // insert images of the character to be in the dialogue
    // I had 3 placeholder images for 3 characters

    // In Dialogue Manager, change the size of Character_Images to fit the cutscene/diagogue
    // insert the images of character under DialogueBox into the the array in Dialogue Manager

    // In checkpoints or any triggerable dialogue object
    // Insert the dialogue_trigger script
    // Input the number of total sentences into Sentences array
    // Insert the sentences

    // In the "Next_player_index" array of the "Dialogue_Trigger" script
    // insert how many characters bounce off each other in Dialogue

    // For example

    // Kep: Hi, I'm Kep
    // Kep: Nice to meet ya
    // Rob: Hello there, Kep!
    // Kep: Gee wiz
    // Kep: Golly Gorsh
    // 3rd Guy: Hi, I'm a 3rd person!

    // This has a sequence of 4 characters: Kep -> Rob -> Kep -> 3rd Guy
    // saying things
    // Think of the whole scene as an array
    // Match the dialogue scene array (above) to "Next_player_index" in the index position of what person says what

    // For example

    // Kep: 0       b/c he starts saying his line at index 0
    // Rob: 2       b/c he starts saying his lines at index 2
    // Kep: 3       b/c he starts again at index 3
    // 3rd Guy: 5   b/c he starts saying his lines at index 5

    // "Next_player_index" = [0, 2, 3, 5]
    // Do this in the inspector of the Dialogue Trigger Script

    // Using the example above

    // The size of "Player_names" has to match the size of "Next_player_index"
    // The "Player_names" is the sequence in which names appear
    // So Kep -> Rob -> Kep -> 3rd Guy
    // "Player_names" = ["Kep", "Rob", "Kep", "3rd Guy"]
    //
}
