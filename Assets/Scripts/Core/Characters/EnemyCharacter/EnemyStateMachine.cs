using System;
using System.Collections.Generic;
using Core.Characters.EnemyCharacter.EnemyStates;

namespace Core.Characters.EnemyCharacter
{
    public class EnemyStateMachine
    {
        private Dictionary<Type, IEnemyState> _states;
        private IEnemyState _activeState;
      

        public EnemyStateMachine(Emeny enemy)
        {
            _states = new Dictionary<Type, IEnemyState>
            {
                { typeof(EnemyMoveState), new EnemyMoveState(enemy, this) },
                { typeof(EnemyAttackState), new EnemyAttackState(enemy, this) },
                { typeof(EnemyDieState), new EnemyDieState(enemy) },
            };

            Enter<EnemyMoveState>();
        }

        public void Update()
        {
            _activeState?.Update();
        }

        public void Enter<TState>() where TState : class, IEnemyState
        {
            IEnemyState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IEnemyState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IEnemyState =>
            _states[typeof(TState)] as TState;
    }
}