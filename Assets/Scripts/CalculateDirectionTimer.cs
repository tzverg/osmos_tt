using UnityEngine;

public class CalculateDirectionTimer : MonoBehaviour
{
    [SerializeField]
    private float calculateDirectionStartTime;
    [SerializeField]
    private float calculateDirectionIntervalMinTime;
    [SerializeField]
    private float calculateDirectionIntervalMaxTime;
    [SerializeField]
    private float calculateDirectionIntervalTime;

    public float CalculateDirectionStartTime { get { return calculateDirectionStartTime; }}
    public float CalculateDirectionIntervalTime { get { return calculateDirectionIntervalTime; }}

    public void UpdateCalculateDirectionIntervalTime()
    {
        calculateDirectionIntervalTime = UnityEngine.Random.Range(calculateDirectionIntervalMinTime, calculateDirectionIntervalMaxTime);
    }
}