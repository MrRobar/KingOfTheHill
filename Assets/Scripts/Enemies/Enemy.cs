using System;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] protected EnemiesPool _enemiesPool;

    public EnemiesPool EnemiesPool
    {
        get { return _enemiesPool; }
        set { _enemiesPool = value; }
    }
    public virtual void Activate()
    {
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().GameOver();
            gameObject.SetActive(false);
        }
    }
}