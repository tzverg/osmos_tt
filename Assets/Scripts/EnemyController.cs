using UnityEngine;

enum AIBehavior { SEARCH, HUNTING, RUNAWAY }

public class EnemyController : UnitController
{
    private AIBehavior aiBehavior;
    private Transform enemyTarget;
    private float timePast;

    void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        aiBehavior = AIBehavior.SEARCH;
        CalculateMotionParams();
        CalculateTargetDirection(Vector3.zero);
        transfomData.startValue = transform.localScale;
        transfomData.transformTime = 2F;
        timePast = 0;
    }

    private void UpdateDirection()
    {
        if (aiBehavior == AIBehavior.SEARCH)
        {
            CalculateRandomDirection();
            CalculateDirectionTimer.UpdateCalculateDirectionIntervalTime();
        }
        else
        {
            CalculateTargetDirection(enemyTarget.position);
        }
    }

    override public void CalculateTargetDirection(Vector3 targertPosition)
    {
        switch (aiBehavior)
        {
            case AIBehavior.SEARCH:
                {
                    CalculateRandomDirection();
                    break;
                }
            case AIBehavior.HUNTING:
                {
                    MoveDirection = (targertPosition - transform.position).normalized;
                    break;
                }
            case AIBehavior.RUNAWAY:
                {
                    MoveDirection = (targertPosition - transform.position).normalized * -1;
                    break;
                }
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null || collision.gameObject.GetComponent<EnemyController>() != null)
        {
            enemyTarget = collision.transform;

            if (IsBiggerThanMe(collision.transform.localScale))
            {
                aiBehavior = AIBehavior.RUNAWAY;
            }
            else
            {
                aiBehavior = AIBehavior.HUNTING;
            }

            enableSpeedModifiers = true;
            CalculateTargetDirection(enemyTarget.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.Equals(enemyTarget))
        {
            aiBehavior = AIBehavior.SEARCH;
            enableSpeedModifiers = false;
            CalculateTargetDirection(Vector3.zero);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null || collision.gameObject.GetComponent<EnemyController>() != null)
        {
            Vector3 targetLocalScale = collision.transform.localScale;
            if(IsBiggerThanMe(targetLocalScale))
            {
                transfomData.endValue = Vector3.zero;
                transfomData.disableAfterTransform = true;
            }
            else
            {
                transfomData.endValue = transform.localScale + targetLocalScale;
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
        timePast += Time.fixedDeltaTime;
        if (timePast > CalculateDirectionTimer.intervalTime)
        {
            UpdateDirection();
            timePast = 0;
        }
        Move();

        Breaking();
    }
}