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
    public Vector3 MoveDirection { get { return moveDirection; } set { moveDirection = value; } }
    public TransfomData transfomData;
    public bool enableSpeedModifiers = false;

    private Vector2 moveDirection;
    [SerializeField][Range(1,3)]
    private float baseAcceleration = 1F;
    [SerializeField][Range(0F, 1F)]
    private float resistanceModifier = 0.5F;
    [SerializeField][Range(0F, 1F)]
    private float accelerationModifier = 0.5F;

    private float acceleration;
    private float resistance;

    protected Rigidbody2D RigidBody { get; set; }

    virtual public void CalculateRandomDirection()
    {
        moveDirection = Random.insideUnitCircle;
    }

    protected void CalculateMotionParams()
    {
        acceleration = baseAcceleration / transform.localScale.x;
        resistance = acceleration * 0.1F;
    }

    protected void UnitScale()
    {
        CalculateTransformProcess();
        gameObject.transform.localScale = Vector3.Lerp(transfomData.startValue, transfomData.endValue, transfomData.transformProcess);

        if (transfomData.transformProcess == 1)
        {
            transfomData.transformScale = false;

            if (transfomData.disableAfterTransform)
            {
                transfomData.disableAfterTransform = false;
                gameObject.SetActive(false);
            }
            else
            {
                transfomData.transformProcess = 0;
                transfomData.startValue = transform.localScale;
                CalculateMotionParams();
            }
        }
    }

    private void CalculateTransformProcess()
    {
        transfomData.transformProcess += transfomData.transformTime * Time.deltaTime;
        transfomData.transformProcess = Mathf.Clamp01(transfomData.transformProcess + transfomData.transformTime * Time.deltaTime);
    }

    virtual public void CalculateTargetDirection(Vector3 targetPosition) { }

    public void Move()
    {
        if (enableSpeedModifiers)
        {
            RigidBody.AddForce(moveDirection * (acceleration + accelerationModifier), ForceMode2D.Force);
            if (RigidBody.velocity.magnitude == 0)
            {
                RigidBody.AddForce((Random.insideUnitCircle) * (acceleration + accelerationModifier) * 5, ForceMode2D.Impulse);
            }
        }
        else
        {
            RigidBody.AddForce(moveDirection * acceleration, ForceMode2D.Force);
        }
    }

    protected void Breaking()
    {
        if (Vector2.Distance(RigidBody.velocity, Vector2.zero) > 0)
        {
            if (enableSpeedModifiers)
            {
                RigidBody?.AddForce(-RigidBody.velocity * (resistance + resistanceModifier), ForceMode2D.Force);
            }
            else
            {
                RigidBody.AddForce(-RigidBody.velocity * resistance, ForceMode2D.Force);
            }
        }
    }

    protected bool IsBiggerThanMe(Vector3 targetLocalScale)
    {
        Vector3 unitLocalScale = transform.localScale;
        return targetLocalScale.x > unitLocalScale.x && targetLocalScale.y > unitLocalScale.y;
    }
}