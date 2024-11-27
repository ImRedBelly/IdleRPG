using UnityEngine;

namespace Core.Characters.EnemyCharacter.EnemyStates
{
    public class EnemyMoveState : IEnemyState
    {
        private Emeny _enemy;
        private EnemyStateMachine _enemyStateMachine;

        public EnemyMoveState(Emeny enemy, EnemyStateMachine stateMachine)
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

            var targetPosition = _enemy.Player.Position;
            _enemy.transform.position = Vector3.MoveTowards(
                _enemy.transform.position,
                targetPosition,
                _enemy.MoveSpeed * Time.deltaTime
            );

            var distance = Vector3.Distance(_enemy.transform.position, targetPosition);
            if (distance < _enemy.DistanceForKillPlayer)
            {
                _enemyStateMachine.Enter<EnemyAttackState>();
            }
        }

        public void Exit()
        {
        }
    }
}