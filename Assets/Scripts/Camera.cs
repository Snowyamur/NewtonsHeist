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

        float new_x = player.transform.position.x;
        float new_y = player.transform.position.y;
        transform.position = new Vector3(new_x, new_y, transform.position.z);
        //sData = GameObject.FindGameObjectWithTag("Canvas").GetComponent<SceneData>();

        minX = GameObject.FindGameObjectWithTag("Canvas").GetComponent<SceneData>().minX;
        maxX = GameObject.FindGameObjectWithTag("Canvas").GetComponent<SceneData>().maxX;
        minY = GameObject.FindGameObjectWithTag("Canvas").GetComponent<SceneData>().minY;
        maxY = GameObject.FindGameObjectWithTag("Canvas").GetComponent<SceneData>().maxY;
    }

    // Update is called once per frame
    void Update()
    {
        float temp_x = player.transform.position.x;
        float temp_y = player.transform.position.y;
        Vector3 temp_pos = new Vector3(temp_x, temp_y, transform.position.z);
        newPos = temp_pos + offset; //Update new position

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
