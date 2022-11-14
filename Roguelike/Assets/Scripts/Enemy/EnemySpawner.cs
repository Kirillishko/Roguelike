using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private DamagePopUpCreator _damagePopUpCreator;
    
    private int _maxEnemiesSpawned = 15;
    private int _maxEnemiesAtTime = 5;
    private int _enemiesSpawned = 0;
    private int _currentEnemiesAtTime = 0;
    private float _timeBetweenSpawn = 5;
    private float _currentTime = 0;
    private bool _isSpawning = true;
    
    

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
        enemy.Init(_player);
        enemy.Die += OnDie;
        enemy.Damaged += _damagePopUpCreator.Create;
    }

    private void OnDie()
    {
        _currentEnemiesAtTime--;
    }
}
