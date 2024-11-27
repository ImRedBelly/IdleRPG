using System;
using Core.UI;
using Zenject;
using UnityEngine;
using Core.Configs;
using Core.Characters.Interfaces;

namespace Core.Characters.EnemyCharacter
{
    public class Emeny : MonoBehaviour, IDamagable
    {
        public event Action<Emeny> OnDie;

        [SerializeField] private HealthBar healthBar;

        public IPlayer Player { get; private set; }
        public int GoldReward { get; private set; }
        public float MoveSpeed { get; private set; }
        public float DistanceForKillPlayer { get; private set; }

        private EnemyStateMachine _stateMachine;
        private EnemyHp _enemyHp;
        public bool IsDead => _enemyHp.CurrentHealth <= 0;

        [Inject]
        public void Construct(IPlayer player)
        {
            Player = player;
        }

        public void Initialize(GameConfig config)
        {
            GoldReward = config.goldPerEnemy;
            MoveSpeed = config.enemyMoveSpeed;
            DistanceForKillPlayer = config.distanceForKillPlayer;

            _stateMachine = new EnemyStateMachine(this);
            _enemyHp = new EnemyHp(healthBar, config.enemyHealth);
        }

        private void Update() => _stateMachine.Update();

        public void ApplyDamage(float damage) => _enemyHp.ApplyDamage(damage);
        public void Die() => OnDie?.Invoke(this);
    }
}