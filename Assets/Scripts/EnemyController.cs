using UnityEngine;

public class EnemyController : UnitController
{
    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        CalculateMotionParams();
        CalculateDirection();
    }

    private void FixedUpdate()
    {
        Move();

        Breaking();
    }
}