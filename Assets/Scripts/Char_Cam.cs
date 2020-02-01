using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Cam : MonoBehaviour
{
    // Attach this script to the Camera
    // Drag and drop the player onto the "main_char" attribute of the script in the inspector
    public GameObject main_char;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - main_char.transform.position;
    }

    void Update()
    {
        // update camera to position of tank
        transform.position = main_char.transform.position + offset;
    }
}
