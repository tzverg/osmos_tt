using UnityEngine;

public class PlayerController : UnitController
{
    private Camera mainCamera;

    void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        CalculateMotionParams();
        transfomData.startValue = transform.localScale;
        transfomData.transformTime = 2F;
    }

    override public void CalculateTargetDirection(Vector3 targetPosition)
    {
        MoveDirection = (transform.position - targetPosition).normalized;
    }

    private Vector3 CalculateMousePosition()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyController>() != null)
        {
            Vector3 targetLocalScale = collision.transform.localScale;
            if (targetLocalScale.x <= transform.localScale.x && targetLocalScale.y <= transform.localScale.y)
            {
                transfomData.endValue = transform.localScale + targetLocalScale;
            }
            else
            {
                transfomData.endValue = Vector3.zero;
                transfomData.disableAfterTransform = true;
            }
            transfomData.transformScale = true;
        }
    }

    private void Update()
    {
        if (transfomData.transformScale)
        {
            UnitScale();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            CalculateTargetDirection(CalculateMousePosition());
            Move();
        }

        Breaking();
    }
}