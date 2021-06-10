using UnityEngine;

public class EnemyController : UnitController
{
    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        CalculateMotionParams();
        CalculateDirection();
        transfomData.startValue = transform.localScale;
        transfomData.transformTime = 2F;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null || collision.gameObject.GetComponent<EnemyController>() != null)
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
        Move();

        Breaking();
    }
}