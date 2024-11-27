using Core.UI;

namespace Core.Characters.EnemyCharacter
{
    public class EnemyHp
    {
        public float CurrentHealth { get; private set; }
        private readonly float _maxHealth;

        private HealthBar _healthBar;

        public EnemyHp(HealthBar healthBar, float initHealth)
        {
            _maxHealth = initHealth;
            CurrentHealth = initHealth;
            _healthBar = healthBar;
            
            _healthBar.SetHealth(CurrentHealth / _maxHealth);
        }

        public void ApplyDamage(float damage)
        {
            CurrentHealth -= damage;
            _healthBar.SetHealth(CurrentHealth / _maxHealth);
        }
    }
}