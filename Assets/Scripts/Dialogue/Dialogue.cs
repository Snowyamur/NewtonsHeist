using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    // Character name + their lines
    [SerializeField] public string char_name;
    [TextArea(3,10)] 
    [SerializeField] public string[] sentences;

}
