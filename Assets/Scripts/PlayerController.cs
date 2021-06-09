using UnityEngine;

struct TransfomData
{
    public Vector3 startValue;
    public Vector3 endValue;

    public float transformTime;
    public float transformProcess;
}

public class PlayerController : UnitController
{
    private bool transformScale = false;
    private bool disableAfterTransform = false;

    private TransfomData transfomData;

    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();

        transfomData.startValue = transform.localScale;
        transfomData.transformTime = 1F;
    }

    override public void CalculateDirection()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = transform.position - mouseWorldPosition;
        moveDirection = direction.normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 targetLocalScale = collision.transform.localScale;
        if (targetLocalScale.x >= transform.localScale.x && targetLocalScale.y >= transform.localScale.y)
        {
            transfomData.endValue = Vector3.zero;
            disableAfterTransform = true;
        }
        else
        {
            transfomData.endValue = transform.localScale + targetLocalScale;
        }
        transformScale = true;
    }

    private void UnitTransform()
    {
        CalculateTransformProcess();
        gameObject.transform.localScale = Vector3.Lerp(transfomData.startValue, transfomData.endValue, transfomData.transformProcess);

        if (transfomData.transformProcess == 1)
        {
            transformScale = false;
            if (disableAfterTransform)
            {
                disableAfterTransform = false;
                gameObject.SetActive(false);
            }
        }
    }

    private void CalculateTransformProcess()
    {
        transfomData.transformProcess += transfomData.transformTime * Time.deltaTime;
        transfomData.transformProcess = Mathf.Clamp01(transfomData.transformProcess + transfomData.transformTime * Time.deltaTime);
    }

    private void Update()
    {
        if (transformScale)
        {
            UnitTransform();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            CalculateDirection();
            Move();
        }
    }
}