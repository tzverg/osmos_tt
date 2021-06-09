using UnityEngine;

public class SceneController : MonoBehaviour
{
    private Camera orthoCamera;

    void Start()
    {
        orthoCamera = Camera.main;

        CreateSceneEdgeCollider();
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