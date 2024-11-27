namespace Core.Characters.EnemyCharacter.EnemyStates
{
    public class EnemyDieState : IEnemyState
    {
        private Emeny _enemy;

        public EnemyDieState(Emeny enemy)
        {
            _enemy = enemy;
        }
        
        public void Enter()
        {
            _enemy.Die();
        }

        public void Update() { }
        public void Exit() { }
    }
}