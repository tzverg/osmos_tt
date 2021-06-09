using UnityEngine;

public class EnemyController : UnitController
{
    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        CalculateDirection();
    }

    private void FixedUpdate()
    {
        //Move();
    }
}