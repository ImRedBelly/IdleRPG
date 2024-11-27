namespace Core.Characters.EnemyCharacter.EnemyStates
{
    public interface IEnemyState
    {
        void Enter();
        void Update();
        void Exit();
    }
}