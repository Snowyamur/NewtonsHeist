using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RotationScript))]
public class RotationEditor : Editor
{
    private SerializedProperty isCycle;
    private SerializedProperty isClockwise;
    private SerializedProperty startAngle;
    private SerializedProperty endAngle;
    private SerializedProperty rotationTime;

    private void OnEnable()
    {
        isCycle = serializedObject.FindProperty("isCycle");
        isClockwise = serializedObject.FindProperty("isClockwise");
        startAngle = serializedObject.FindProperty("startAngle");
        endAngle = serializedObject.FindProperty("endAngle");
        rotationTime = serializedObject.FindProperty("rotationTime");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(isCycle, new GUIContent("Is Cycle"));
        if (isCycle.boolValue)
        {
            EditorGUILayout.PropertyField(isClockwise, new GUIContent("Is Clockwise"));
        }
        else
        {
            EditorGUILayout.PropertyField(startAngle, new GUIContent("Start Angle"));
            EditorGUILayout.PropertyField(endAngle, new GUIContent("End Angle"));
        }
        EditorGUILayout.PropertyField(rotationTime, new GUIContent("Rotation Time"));

        serializedObject.ApplyModifiedProperties();
    }
}


// [ExecuteInEditMode]
[System.Serializable]
public class RotationScript : MonoBehaviour
{
    // Public variables
    public bool isCycle = false;
    public bool isClockwise = true;

    public float startAngle = 0f;
    public float endAngle   = 90f;
    public float rotationTime = 2f;

    // Private variables
    private float angleToRotate;
    private float rotationSpeed;

    private void Start()
    {
        angleToRotate = endAngle - startAngle;
        transform.rotation = Quaternion.Euler(0, 0, startAngle);

        rotationSpeed = 360f / rotationTime;
    }

    private void Update()
    {
        if (isCycle)
            Spin();
        else
            PingPong();
    }

    private void PingPong()
    {
        float lerp = 0.5f * (1.0F + Mathf.Sin(Mathf.PI * Time.time / rotationTime));
        float rotateFromStart = angleToRotate * lerp;

        Quaternion target = Quaternion.Euler(0, 0, startAngle + rotateFromStart);
        transform.rotation = target;
    }

    private void Spin()
    {
        if (isClockwise)
            transform.RotateAround(transform.position, Vector3.forward, -rotationSpeed * Time.deltaTime);
        else
            transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }


    // ----- INSPECTOR FUNCTIONS -----
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
