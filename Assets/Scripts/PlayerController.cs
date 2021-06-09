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
        Vector3 targetLocalScale = collision.transform.localScale;
        if (targetLocalScale.x >= transform.localScale.x && targetLocalScale.y >= transform.localScale.y)
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

    private void UnitScale()
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
        }
    }

    private void CalculateTransformProcess()
    {
        transfomData.transformProcess += transfomData.transformTime * Time.deltaTime;
        transfomData.transformProcess = Mathf.Clamp01(transfomData.transformProcess + transfomData.transformTime * Time.deltaTime);
    }

    private void Update()
    {
        if (transfomData.transformScale)
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

        Breaking();
    }
}