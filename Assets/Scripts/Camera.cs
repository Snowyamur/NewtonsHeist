using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera : MonoBehaviour
{
    GameObject player;
    SceneData sData;

    [Header("Camera Coordinates")]
    [SerializeField] float minX; //Booundaries of level
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 newPos;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
        sData = GameObject.FindGameObjectWithTag("SceneData").GetComponent<SceneData>();

        minX = sData.minX;
        maxX = sData.maxX;
        minY = sData.minY;
        maxY = sData.maxY;
    }

    // Update is called once per frame
    void Update()
    {
        newPos = player.transform.position + offset;

        if(maxX >= newPos.x && newPos.x >= minX && maxY >= newPos.x && newPos.x >= minY)
        {
            transform.position = newPos;
        }
    }
}
