using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PoolSetup poolSetup;
    [SerializeField] private int gridSizeX;
    [SerializeField] private int gridSizeY;

    private Vector2[] gridVerticies;

    private Camera orthoCamera;

    void Awake()
    {
        orthoCamera = Camera.main;

        CreateSceneEdgeCollider();
    }

    private void CreateEnemyList()
    {
        
    }

    private void CreateGenerationGrid(Vector2[] borderVerticies)
    {
        for (int cntX = 0; cntX < gridSizeX; cntX++)
        {
            for (int cntY = 0; cntY < gridSizeY; cntY++)
            {

            }
        }
    }

    private void CreateSceneEdgeCollider()
    {
        Vector2 leftBottomCorner = orthoCamera.ViewportToWorldPoint(new Vector3(0, 0));

        Vector2[] borderVertices =
        {
            new Vector2(leftBottomCorner.x, leftBottomCorner.y),
            new Vector2(-leftBottomCorner.x, leftBottomCorner.y),
            new Vector2(-leftBottomCorner.x, -leftBottomCorner.y),
            new Vector2(leftBottomCorner.x, -leftBottomCorner.y),
            new Vector2(leftBottomCorner.x, leftBottomCorner.y)
        };

        EdgeCollider2D borderCollider = gameObject.AddComponent<EdgeCollider2D>();
        borderCollider.points = borderVertices;
    }
}