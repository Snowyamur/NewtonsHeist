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

        //If new postion is within level bounds, update to it
        if(maxX >= newPos.x && newPos.x >= minX && maxY >= newPos.y && newPos.y >= minY)
        {
            transform.position = newPos;
        }
        //If camera hits x bounds of level
        else if(maxY >= newPos.y && newPos.y >= minY)
        {
            transform.position = new Vector3(transform.position.x, newPos.y, transform.position.z);
        }
        //If camera hits y bounds of level
        else if(maxX >= newPos.x && newPos.x >= minX)
        {
            transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);
        }
        //If camera hits both x and y bounds of level, stay put
    }
}
