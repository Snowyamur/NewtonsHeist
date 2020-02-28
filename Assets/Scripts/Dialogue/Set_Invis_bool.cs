using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Invis_bool : MonoBehaviour
{
    public bool can_trigger_invisible_dialogue = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        can_trigger_invisible_dialogue = true;
    }
}
