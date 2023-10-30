using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : MonoBehaviour
{
    [SerializeField] private int _poolSize = 5;
    [SerializeField] private List<GameObject> _enemiesPool;

    private void Awake()
    {
        _enemiesPool = new List<GameObject>(_poolSize);
    }

    public void Remove(GameObject enemy)
    {
        _enemiesPool.Remove(enemy);
    }

    public void Add(GameObject enemy)
    {
        enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _enemiesPool.Add(enemy);
    }

    public void AddPoolReference()
    {
        foreach (var enemy in _enemiesPool)
        {
            enemy.GetComponent<Enemy>().EnemiesPool = this;
        }
    }

    public void DisableAll()
    {
        foreach (var enemy in _enemiesPool)
        {
            enemy.SetActive(false);
        }
    }

    public GameObject TakeFromPool()
    {
        return _enemiesPool[0];
    }

    public int Count()
    {
        return _enemiesPool.Count;
    }
}