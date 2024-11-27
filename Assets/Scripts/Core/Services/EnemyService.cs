using System;
using Zenject;
using Core.Utils;
using UnityEngine;
using Core.Configs;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Core.Characters;
using Core.Characters.EnemyCharacter;
using Core.Characters.Interfaces;
using Core.Services.Interfaces;
using Random = UnityEngine.Random;

namespace Core.Services
{
    public class EnemyService : IInitializable, IDisposable, IEnemyService
    {
        private GameConfig _config;
        private GameStateService _gameStateService;
        private IGoldService _goldService;

        private ObjectPool<Emeny> _enemyPool;
        private readonly List<Emeny> _activeEnemies = new();
        private CancellationTokenSource _cancellationTokenSource;

        private int _enemiesKilled = 0;

        public EnemyService(GameConfig config, GameStateService gameStateService, IGoldService goldService)
        {
            _config = config;
            _gameStateService = gameStateService;
            _goldService = goldService;
        }

        public void Initialize()
        {
            _enemyPool = new ObjectPool<Emeny>(_config.emenyPrefab);
            _gameStateService.OnChangeGameState += ChangeGameState;
        }

        public void Dispose()
        {
            _gameStateService.OnChangeGameState -= ChangeGameState;
            _cancellationTokenSource?.Dispose();
        }

        public IDamagable GetClosestEnemy(Vector3 positionPlayer)
        {
            float distance = float.MaxValue;
            Emeny nearEmeny = null;

            foreach (var activeEnemy in _activeEnemies)
            {
                var currentDistance = (activeEnemy.transform.position - positionPlayer).sqrMagnitude;
                if (distance > currentDistance)
                {
                    distance = currentDistance;
                    nearEmeny = activeEnemy;
                }
            }

            return nearEmeny;
        }

        private void ChangeGameState(GameState stateGame)
        {
            if (stateGame == GameState.Playing)
                StartSpawnEnemies();
            else
            {
                OnCancelSpawnEnemies();
                RemoveAllEnemy();
            }
        }

        private async void StartSpawnEnemies()
        {
            _enemiesKilled = 0;
            OnCancelSpawnEnemies();
            _cancellationTokenSource = new CancellationTokenSource();

            while (_cancellationTokenSource != null && !_cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    SpawnEnemy();
                    float delay = Random.Range(_config.minEnemySpawnDelay, _config.maxEnemySpawnDelay);
                    await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: _cancellationTokenSource.Token);
                }
                catch (Exception ex)
                {
                    Debug.Log("Spawn enemies is ended");
                }
            }
        }

        private void SpawnEnemy()
        {
            Emeny emeny = _enemyPool.Get();


            emeny.Initialize(_config);
            emeny.transform.position = new Vector3(3.5f, Random.Range(-4, 4), 0);
            emeny.OnDie += EnemyDied;
            _activeEnemies.Add(emeny);
        }


        private void EnemyDied(Emeny emeny)
        {
            RemoveEnemy(emeny);
            _enemiesKilled++;
            _goldService?.AddGold(emeny.GoldReward);
            if (_enemiesKilled >= _config.enemiesToWin)
                _gameStateService.Victory();
        }


        private void RemoveAllEnemy()
        {
            for (int i = _activeEnemies.Count - 1; i >= 0; i--)
            {
                RemoveEnemy(_activeEnemies[i]);
            }
        }

        private void RemoveEnemy(Emeny emeny)
        {
            emeny.OnDie -= EnemyDied;
            _activeEnemies.Remove(emeny);
            _enemyPool.Release(emeny);
        }


        private void OnCancelSpawnEnemies()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    }
}