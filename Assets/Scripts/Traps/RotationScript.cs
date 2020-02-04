using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class RotationScript : MonoBehaviour
{
    // [SerializeField] float angleOfRotation = 90f;
    [SerializeField] float startAngle = 0f;
    [SerializeField] float endAngle   = 90f;
    [SerializeField] float rotationTime = 2f;

    private void Update()
    {
        Quaternion from = Quaternion.Euler(0, 0, endAngle);
        Quaternion to = Quaternion.Euler(0, 0, startAngle);

        float lerp = 0.5F * (1.0F + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup / rotationTime));
        transform.localRotation = Quaternion.Lerp(from, to, lerp);
    }

    private void OnDrawGizmos()
    {
        DrawRotationPath();
    }

    private void DrawRotationPath()
    {
        Gizmos.color = Color.red; 

        Mesh mesh = new Mesh();

        /*
        // Initialize mesh arrays
        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        vertices[0] = Vector3.zero;
        vertices[1] = GetVectorFromAngle(startAngle);
        vertices[2] = GetVectorFromAngle(endAngle);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        Gizmos.DrawMesh(mesh);
        */
    }


    // ----- HELPER FUNCTIONS -----
    private float DegreesToRad(float degree)
    {
        return degree * Mathf.PI / 180.0f;
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float radian = DegreesToRad(angle);
        return new Vector3(Mathf.Cos(radian), Mathf.Sin(radian));
    }
}
