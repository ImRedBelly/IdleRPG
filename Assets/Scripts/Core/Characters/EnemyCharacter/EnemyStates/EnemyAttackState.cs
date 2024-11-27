using UnityEngine;

namespace Core.Characters.EnemyCharacter.EnemyStates
{
    public class EnemyAttackState : IEnemyState
    {
        private Emeny _enemy;
        private EnemyStateMachine _enemyStateMachine;

        public EnemyAttackState(Emeny enemy, EnemyStateMachine stateMachine)
        {
            _enemy = enemy;
            _enemyStateMachine = stateMachine;
        }

        public void Enter()
        {
        }

        public void Update()
        {
            if (_enemy.IsDead)
            {
                _enemyStateMachine.Enter<EnemyDieState>();
                return;
            }

            _enemy.Player.ApplyDamage(1);

            if (Vector3.Distance(_enemy.transform.position, _enemy.Player.Position) > _enemy.DistanceForKillPlayer)
            {
                _enemyStateMachine.Enter<EnemyMoveState>();
            }
        }

        public void Exit()
        {
        }
    }
}