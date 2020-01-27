using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RotationScript : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 10);
        */
        DrawRotationPath();
    }

    private void DrawRotationPath()
    {
        Mesh mesh = new Mesh();

        // Initialize mesh arrays
        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        vertices[0] = Vector3.zero;
        vertices[1] = new Vector3(200, 0);
        vertices[2] = new Vector3(0, -200);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        Gizmos.DrawMesh(mesh);
    }
}
