using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera : MonoBehaviour
{
    GameObject player;

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
        //sData = GameObject.FindGameObjectWithTag("Canvas").GetComponent<SceneData>();

        minX = GameObject.FindGameObjectWithTag("Canvas").GetComponent<SceneData>().minX;
        maxX = GameObject.FindGameObjectWithTag("Canvas").GetComponent<SceneData>().maxX;
        minY = GameObject.FindGameObjectWithTag("Canvas").GetComponent<SceneData>().minY;
        maxY = GameObject.FindGameObjectWithTag("Canvas").GetComponent<SceneData>().maxY;
    }

    // Update is called once per frame
    void Update()
    {
        newPos = player.transform.position + offset; //Update new position

        if (maxX < newPos.x)
            newPos.x = maxX;
        else if (newPos.x < minX)
            newPos.x = minX;

        if (maxY < newPos.y)
            newPos.y = maxY;
        else if (newPos.y < minY)
            newPos.y = minY;

        transform.position = newPos;
    }
}
