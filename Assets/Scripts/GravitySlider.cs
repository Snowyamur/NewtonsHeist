using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GravitySlider : MonoBehaviour
{
    Slider gravSlider;

    void Start()
    {
        gravSlider = (Slider) FindObjectOfType(typeof (Slider));
    }

    void Update()
    {
        gravSlider.value = LevelManager.current.playerData.gravityPower;
    }
}
