using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private Transform[] _spawnPoints;
    private int _maxEnemiesSpawned;
    private int _maxEnemiesAtTime;
    private int _enemiesSpawned;
    private int _currentEnemiesAtTime;
    private float _timeBetweenSpawn;
    private float _currentTime;

    private bool _isSpawning;

    private void Update()
    {
        if (_isSpawning == false)
            return;

        if (_enemiesSpawned >= _maxEnemiesSpawned || _currentEnemiesAtTime >= _maxEnemiesAtTime)
            return;

        if (_currentTime < _timeBetweenSpawn)
        {
            _currentTime += Time.deltaTime;
            return;
        }

        _currentTime = 0f;
        _enemiesSpawned++;
        _currentEnemiesAtTime++;
        Spawn();
    }

    private void Spawn()
    {
        var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        var enemy = _enemies[Random.Range(0, _enemies.Length)];

        enemy = Instantiate(enemy, spawnPoint);
        enemy.Die += OnDie;
    }

    private void OnDie()
    {
        _currentEnemiesAtTime--;
    }
}
