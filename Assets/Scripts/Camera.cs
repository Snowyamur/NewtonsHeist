using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject character;
    [SerializeField] private Vector3 offset;
    void Start()
    {
        offset = transform.position - character.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = character.transform.position + offset;
    }
}
