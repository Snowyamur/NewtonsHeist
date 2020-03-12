using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    [Header("Detection")]
    [Range(0.0f, 179.0f)]
    [SerializeField] float detectionConeAngle = 90.0f;
    [SerializeField] float detectionRayDistance = 5.0f;
    [SerializeField] int numberOfRaycasts = 2;
    [SerializeField] Color coneColor = Color.yellow;

    // ----- Private variables -----
    // Detection cone variables
    private Mesh mesh;
    private MeshRenderer meshRenderer;
    private float angleIncrement;
    private Vector3 origin = Vector3.zero;
    [SerializeField] private LayerMask layerMask;
    private float startingAngle = 0f;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        meshRenderer = GetComponent<MeshRenderer>();

        angleIncrement = detectionConeAngle / numberOfRaycasts;
    }

    private void Update()
    {
        origin = transform.position;
    }

    private void LateUpdate()
    {
        Detect();
    }

    private void Detect()
    {
        // Initialize mesh arrays
        Vector3[] vertices = new Vector3[numberOfRaycasts + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[numberOfRaycasts * 3];

        // Create the mesh
        vertices[0] = transform.InverseTransformPoint(transform.position);

        int vertexIndex   = 1;
        int triangleIndex = 0;
        float angle = startingAngle;
        for (int i = 0; i <= numberOfRaycasts; i++)
        {
            Vector3 angledVector = GetVectorFromAngle(angle);
            Vector3 vertex;

            RaycastHit2D raycastHit = DrawRaycast(origin, angledVector, detectionRayDistance, layerMask);
            if (raycastHit.collider == null)
            {
                Vector3 worldVertex = origin + angledVector * detectionRayDistance;
                vertex = transform.InverseTransformPoint(worldVertex);
            }
            else
            {
                if (raycastHit.transform.CompareTag("Player"))
                {
                    GameManager.Instance.RespawnPlayer();
                }

                vertex = transform.InverseTransformPoint(raycastHit.point);
            }


            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrement;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();

        meshRenderer.material.color = coneColor;
    }


    // ----- PUBLIC SETTER FUNCTIONS -----

    //Sets direction of cone based on angle
    public void SetAimDirection(Vector2 aimDirection, bool isFacingLeft)
    {
        if(isFacingLeft)
        {
            startingAngle = GetAngleFromVectorFloat(-aimDirection) + detectionConeAngle / 2f;
        }
        else
        {
            startingAngle = GetAngleFromVectorFloat(aimDirection) + detectionConeAngle / 2f;
        }
    }


    // ----- HELPER FUNCTIONS -----

    //Draws the raycast for degugging
    private RaycastHit2D DrawRaycast(Vector2 origin, Vector2 direction, float distance, LayerMask layerMask)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(origin, direction, distance, layerMask);
        Debug.DrawRay(origin, direction.normalized * distance, Color.yellow);

        return raycastHit;
    }

    //Turn degrees to radians
    private float DegreesToRad(float degree)
    {
        return degree * Mathf.PI / 180.0f;
    }

    //Turns an angle to a vector
    private Vector3 GetVectorFromAngle(float angle)
    {
        float radian = DegreesToRad(angle);
        return new Vector3(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    //Converts a vector to an angle
    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }


    /* ----- CODE FROM PREVIOUS VERSION -----
    private void SendRaycasts()
    {
        // Create a cone of detection
        for (float i = -detectionConeAngle / 2; i <= detectionConeAngle / 2; i += angleIncrement)
        {
            Vector2 direction = CalcRayDir(i);
            RaycastHit2D coneRaycast = DrawRaycast(transform.position, direction,
                                                   detectionRayDistance);

            if (coneRaycast.collider != false && coneRaycast.transform.tag == "Player")
            {
                Destroy(coneRaycast.transform.gameObject);

                // TODO: implement checkpoint respawn
            }
        }
    }

    private Vector2 CalcRayDir(float angle)
    {
        // Determine whether to cast rays left or right
        float baseDir;
        if (isFacingLeft)
            baseDir = Vector2.left.x;
        else
            baseDir = Vector2.right.x;

        float radians = DegreesToRad(angle);
        float height = CalcHeight(baseDir, radians);

        Vector2 angledDir = new Vector2(baseDir, height);

        return angledDir;
    }

    private float CalcHeight(float baseLen, float radian)
    {
        // FORMULA: tan(angle) = height / base
        return Mathf.Tan(radian) * baseLen;
    }
    */
}
