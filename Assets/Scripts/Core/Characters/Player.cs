using System;
using System.Threading;
using Core.Characters.Interfaces;
using Core.Configs;
using Core.Services;
using Core.Services.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Core.Characters
{
    public class Player : MonoBehaviour, IPlayer
    {
        public Vector3 Position => transform.position;

        private GameStateService _gameStateService;
        private IEnemyService _enemyService;

        private float _damage;
        private float _attackInterval;

        private CancellationTokenSource _cancellationTokenSource;

        [Inject]
        public void Construct(GameStateService gameStateService, GameConfig config, IEnemyService enemyService)
        {
            _gameStateService = gameStateService;
            _enemyService = enemyService;

            _damage = config.playerDamage;
            _attackInterval = config.playerAttackInterval;
        }

        private void Start()
        {
            _gameStateService.OnChangeGameState += ChangeGameState;
        }

        public void OnDisable()
        {
            _gameStateService.OnChangeGameState -= ChangeGameState;
        }

        private void ChangeGameState(GameState stateGame)
        {
            if (stateGame == GameState.Playing)
                StartAttackNearEnemy();
            else
                OnCancelAttack();
        }


        public void ApplyDamage(float damage)
        {
            _gameStateService.GameOver();
        }

        private async void StartAttackNearEnemy()
        {
            OnCancelAttack();
            _cancellationTokenSource = new CancellationTokenSource();

            while (_cancellationTokenSource != null && !_cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(_attackInterval), cancellationToken: _cancellationTokenSource.Token);
                    var enemy = _enemyService.GetClosestEnemy(transform.position);
                    if (enemy != null)
                    {
                        enemy.ApplyDamage(_damage);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log("Attack is ended");
                }
            }
        }

        private void OnCancelAttack()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    }
}