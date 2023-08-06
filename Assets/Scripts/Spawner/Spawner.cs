using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform[] _spawnPoints; 
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;

    public event UnityAction AllEnemySpawned;

    public void NextWave()
    {
        SetWave(_currentWaveNumber);
        _spawned = 0;
    }

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay && _currentWave.Count != _spawned)
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
        }

        if (_currentWave.Count <= _spawned)
        {
            if (_waves.Count > _currentWaveNumber + 1)
            {
                AllEnemySpawned?.Invoke();
            }
            _currentWave = null;
        }
    }

    private void InstantiateEnemy()
    {
        if (_currentWave.Count > 0)
        {
            int index = Random.Range(0, _currentWave.Count - 1);
            GameObject template = _currentWave.Templates[index];

            Transform randomSpawnPoint = GetRandomSpawnPointNotInCamera();

            Enemy enemy = Instantiate(template, randomSpawnPoint.position, randomSpawnPoint.rotation, randomSpawnPoint).GetComponent<Enemy>();
            enemy.Init(_player);
        }
    }

    private Transform GetRandomSpawnPointNotInCamera()
    {
        List<Transform> eligibleSpawnPoints = new List<Transform>();

        foreach (Transform spawnPoint in _spawnPoints)
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(spawnPoint.position);
            if (screenPoint.x < 0 || screenPoint.x > Screen.width || screenPoint.y < 0 || screenPoint.y > Screen.height)
            {
                eligibleSpawnPoints.Add(spawnPoint);
            }
        }

        if (eligibleSpawnPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, eligibleSpawnPoints.Count);
            return eligibleSpawnPoints[randomIndex];
        }

        return _spawnPoints[Random.Range(0, _spawnPoints.Length)];
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }
}

[System.Serializable]
public class Wave
{
    public List<GameObject> Templates;
    public float Delay;
    public int Count;
}
