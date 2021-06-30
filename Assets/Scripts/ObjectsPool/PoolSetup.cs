﻿using UnityEngine;

public class PoolSetup : MonoBehaviour
{
    [SerializeField] private PoolManager.PoolPart[] pools;

    void OnValidate()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].name = pools[i].prefab.name;
        }
    }

    void Initialize()
    {
        PoolManager.Initialize(pools);
    }

    void Awake()
    {
        Initialize();
    }
}
