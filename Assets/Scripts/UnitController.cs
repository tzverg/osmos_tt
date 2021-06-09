using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class UnitController : MonoBehaviour, IMovable
{
    protected Vector2 moveDirection;

    private float acceleration = 0.5F;
    private float resistance = 5F;

    protected Rigidbody2D RigidBody { get; set; }

    private void Start()
    {
        //resistance = acceleration * 0.5F;
    }

    virtual public void CalculateDirection()
    {
        //moveDirection = new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), 0F);
        Vector2 randomVector = Random.insideUnitCircle;
    }

    public void Move()
    {
        RigidBody.AddForce(moveDirection * acceleration, ForceMode2D.Force);
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(RigidBody.velocity, Vector2.zero) > 0)
        {
            RigidBody.AddForce(-RigidBody.velocity * resistance, ForceMode2D.Force);
        }
    }
}