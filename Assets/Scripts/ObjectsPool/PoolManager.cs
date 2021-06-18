using UnityEngine;

public static class PoolManager
{
    [System.Serializable]
    public struct PoolPart
    {
        public string name;
        public ObjectForPool prefab;
        public int count;
        public ObjectsPool poolLink;
    }

    private static PoolPart[] pools;
    private static GameObject objectsParent;

    public static void Initialize(PoolPart[] newPools)
    {
        pools = newPools;

        objectsParent = new GameObject();
        objectsParent.name = "Pool";

        for (int i = 0; i < pools.Length; i++)
        {
            if (pools[i].prefab != null)
            {
                pools[i].poolLink = new ObjectsPool();
                pools[i].poolLink.Initialize(pools[i].count, pools[i].prefab, objectsParent.transform);
            }
        }
    }

    public static GameObject GetObject(string name, Vector3 position, Quaternion rotation)
    {
        GameObject result = null;

        if (pools != null)
        {
            for (int i = 0; i < pools.Length; i++)
            {
                if (string.Compare(pools[i].name, name) == 0)
                {
                    result = pools[i].poolLink.GetObject().gameObject;

                    result.transform.position = position;
                    result.transform.rotation = rotation;

                    result.SetActive(true);

                    return result;
                }
            }
        }

        return result;
    }
}
