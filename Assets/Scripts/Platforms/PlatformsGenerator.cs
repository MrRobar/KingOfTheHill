using System;
using UnityEngine;

public class PlatformsGenerator : MonoBehaviour
{
    [SerializeField] private Transform _holder;
    [SerializeField] private GameObject _platform;
    [SerializeField] private int _amount;
    [SerializeField] private PlatformsConveyor _conveyor;
    [SerializeField] private GameObject[] _platforms;

    public event Action<int> OnPlatformsSpawned; 

    private void Awake()
    {
        GeneratePlatforms();
    }

    private void GeneratePlatforms()
    {
        ClearHolder();

        _platforms = new GameObject[_amount];
        for (int i = 0; i < _amount; i++)
        {
            var instance = Instantiate(_platform, _holder);
            instance.transform.localPosition = new Vector3(0, i, i);
            instance.name = i.ToString();
            _platforms[i] = instance;
        }

        _conveyor.Platforms = _platforms;
        OnPlatformsSpawned?.Invoke(_amount);
    }

    private void ClearHolder()
    {
        if (_holder.childCount < 1)
        {
            return;
        }

        for (int i = 0; i < _amount; i++)
        {
            DestroyImmediate(_holder.GetChild(0).gameObject);
        }
    }
}