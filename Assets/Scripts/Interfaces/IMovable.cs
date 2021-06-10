using UnityEngine;

interface IMovable
{
    Vector3 MoveDirection { get; set; }

    void Move();
    void CalculateRandomDirection();
    void CalculateTargetDirection(Vector3 targetPosition);
}