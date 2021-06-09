using UnityEngine;

public struct TransfomData
{
    public Vector3 startValue;
    public Vector3 endValue;

    public float transformTime;
    public float transformProcess;

    public bool transformScale;
    public bool disableAfterTransform;
}

[RequireComponent(typeof(Rigidbody2D))]
public class UnitController : MonoBehaviour, IMovable
{
    public TransfomData transfomData;
    protected Vector2 moveDirection;

    [SerializeField][Range(1,3)]
    private float baseAcceleration = 1F;

    private float acceleration;
    private float resistance;

    protected Rigidbody2D RigidBody { get; set; }

    virtual public void CalculateDirection()
    {
        //moveDirection = new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), 0F);
        moveDirection = Random.insideUnitCircle;
    }

    protected void CalculateMotionParams()
    {
        acceleration = baseAcceleration / transform.localScale.x;
        resistance = acceleration * 0.1F;
    }

    public void Move()
    {
        RigidBody.AddForce(moveDirection * acceleration, ForceMode2D.Force);
    }

    protected void Breaking()
    {
        if (Vector2.Distance(RigidBody.velocity, Vector2.zero) > 0)
        {
            RigidBody.AddForce(-RigidBody.velocity * resistance, ForceMode2D.Force);
        }
    }
}