using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;
using Task = System.Threading.Tasks.Task;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private PlatformsGenerator _generator;
    [SerializeField] private int _maxEnemiesPoolSize = 5, _spawnDelay = 5000;
    [SerializeField] private List<GameObject> _enemiesPrefabs;
    [SerializeField] private EnemiesPool _enemiesPool;
    private bool _gameInProcess = true;
    private int _spawnPoint;

    private void Awake()
    {
        SpawnEnemyAsync();
    }

    private void OnEnable()
    {
        _generator.OnPlatformsSpawned += SpawnEnemies;
        _player.GetComponent<PlayerController>().OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        _generator.OnPlatformsSpawned -= SpawnEnemies;
        if (_player != null)
        {
            _player.GetComponent<PlayerController>().OnGameOver -= GameOver;
        }
    }

    private void TakeEnemyFromPool()
    {
        if (_enemiesPool.Count() < 1)
        {
            return;
        }

        var enemy = _enemiesPool.TakeFromPool();
        _enemiesPool.Remove(enemy);
        Vector3 pos = new Vector3(_player.transform.position.x, enemy.transform.position.y + 1.5f,
            enemy.transform.position.z);
        enemy.transform.position = pos;
        enemy.SetActive(true);
        enemy.GetComponent<Enemy>().Activate();
    }

    private void SpawnEnemies(int amount)
    {
        Vector3 pos = new Vector3(0f, amount--, amount--);
        Random random = new Random();
        for (int i = 0; i < _maxEnemiesPoolSize; i++)
        {
            var rndID = random.Next(0, _enemiesPrefabs.Count);
            var enemy = Instantiate(_enemiesPrefabs[rndID], pos, Quaternion.Euler(0, 0, 90));
            enemy.SetActive(false);
            _enemiesPool.Add(enemy);
        }

        _enemiesPool.AddPoolReference();
    }

    private void GameOver()
    {
        _gameInProcess = false;
        _enemiesPool.DisableAll();
        gameObject.SetActive(false);
    }

    private async void SpawnEnemyAsync()
    {
        await DeployNewEnemyTask();
    }

    private async Task DeployNewEnemyTask()
    {
        while (_gameInProcess)
        {
            await Task.Delay(_spawnDelay);
            TakeEnemyFromPool();
        }
    }
}