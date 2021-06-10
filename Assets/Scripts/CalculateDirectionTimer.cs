public static class CalculateDirectionTimer
{
    public static float intervalMinTime = 1F;
    public static float intervalMaxTime = 2F;
    public static float intervalTime;

    public static void UpdateCalculateDirectionIntervalTime()
    {
        intervalTime = UnityEngine.Random.Range(intervalMinTime, intervalMaxTime);
    }
}