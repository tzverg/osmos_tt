using UnityEngine;

public class PlayerController : UnitController
{
    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        CalculateMotionParams();
        transfomData.startValue = transform.localScale;
        transfomData.transformTime = 2F;
    }

    override public void CalculateDirection()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = transform.position - mouseWorldPosition;
        moveDirection = direction.normalized;
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
            CalculateDirection();
            Move();
        }

        Breaking();
    }
}