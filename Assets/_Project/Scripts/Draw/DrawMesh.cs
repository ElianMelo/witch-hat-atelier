using System.Collections.Generic;
using UnityEngine;

public class DrawMesh : MonoBehaviour
{
    public float lineThickness = 1f;
    public float distanceFromTheCamera = 10f;
    public Material lineMaterial;
    public Transform player;
    public GameObject drawedMesh;
    private Mesh currentMesh;
    private List<GameObject> instances = new();
    
    private Vector3 lastMousePosition;

    private void Start()
    {
        GameManager.Instance.OnStartDrawing.AddListener(OnStartDrawing);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStartDrawing.RemoveListener(OnStartDrawing);
    }

    private void OnStartDrawing()
    {
        foreach (var instance in instances)
        {
            DestroyImmediate(instance);
        }
        instances.Clear();
    }

    private void Update()
    {
        if (GameManager.Instance.currentGameState != GameManager.GameState.Drawing) return;
        if (Input.GetMouseButtonDown(0))
        {
            GameObject currentInstance = Instantiate(drawedMesh.gameObject, transform);
            instances.Add(currentInstance);
            // Mouse Pressed
            currentMesh = new Mesh();

            Vector3[] vertices = new Vector3[4];
            Vector2[] uv = new Vector2[4];
            int[] triangles = new int[6];

            vertices[0] = GetMouseWorldPosition();
            vertices[1] = GetMouseWorldPosition();
            vertices[2] = GetMouseWorldPosition();
            vertices[3] = GetMouseWorldPosition();

            uv[0] = Vector2.zero;
            uv[1] = Vector2.zero;
            uv[2] = Vector2.zero;
            uv[3] = Vector2.zero;

            triangles[0] = 0;
            triangles[1] = 3;
            triangles[2] = 1;

            triangles[3] = 1;
            triangles[4] = 3;
            triangles[5] = 2;

            currentMesh.vertices = vertices;
            currentMesh.uv = uv;
            currentMesh.triangles = triangles;
            currentMesh.MarkDynamic();

            currentInstance.GetComponent<MeshFilter>().mesh = currentMesh;
            currentInstance.GetComponent<MeshRenderer>().material = lineMaterial;
            lastMousePosition = GetMouseWorldPosition();
        }
        if (Input.GetMouseButton(0))
        {
            // Mouse held down
            float minDistance = .1f;
            if(Vector3.Distance(GetMouseWorldPosition(), lastMousePosition) > minDistance)
            {
                Vector3[] vertices = new Vector3[currentMesh.vertices.Length + 2];
                Vector2[] uv = new Vector2[currentMesh.uv.Length + 2];
                int[] triangles = new int[currentMesh.triangles.Length + 6];

                currentMesh.vertices.CopyTo(vertices, 0);
                currentMesh.uv.CopyTo(uv, 0);
                currentMesh.triangles.CopyTo(triangles, 0);

                int vIndex = vertices.Length - 4;
                int vIndex0 = vIndex + 0;
                int vIndex1 = vIndex + 1;
                int vIndex2 = vIndex + 2;
                int vIndex3 = vIndex + 3;

                Vector3 mouseForwardVector = (GetMouseWorldPosition() - lastMousePosition).normalized;
                // Vector3 normal2D = new Vector3(0f, 0f, -1f);
                Vector3 normal2D = Camera.main.transform.position - player.transform.position;
                Vector3 newVertexUp = GetMouseWorldPosition() + Vector3.Cross(mouseForwardVector, normal2D) * lineThickness;
                Vector3 newVertexDown = GetMouseWorldPosition() + Vector3.Cross(mouseForwardVector, normal2D * -1f) * lineThickness;
                //debugVisual1.position = newVertexUp;
                //debugVisual2.position = newVertexDown;

                vertices[vIndex2] = newVertexUp;
                vertices[vIndex3] = newVertexDown;

                uv[vIndex2] = Vector2.zero;
                uv[vIndex3] = Vector2.zero;

                int tIndex = triangles.Length - 6;

                triangles[tIndex + 0] = vIndex0;
                triangles[tIndex + 1] = vIndex2;
                triangles[tIndex + 2] = vIndex1;

                triangles[tIndex + 3] = vIndex1;
                triangles[tIndex + 4] = vIndex2;
                triangles[tIndex + 5] = vIndex3;

                currentMesh.vertices = vertices;
                currentMesh.uv = uv;
                currentMesh.triangles = triangles;

                lastMousePosition = GetMouseWorldPosition();
            }
        }
        //transform.position = GetMouseWorldPosition();
    }

    private Vector3 GetMouseWorldPosition()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = distanceFromTheCamera; // select distance units from the camera
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
