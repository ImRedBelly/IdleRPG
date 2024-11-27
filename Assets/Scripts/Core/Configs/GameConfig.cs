using Core.Characters;
using Core.Characters.EnemyCharacter;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config/GameConfig", order = 1)]
    public class GameConfig : ScriptableObject
    {
        public Player playerPrefab;
        public Emeny emenyPrefab;
        [Space]
        public float playerAttackInterval;
        public float playerDamage;
        public float playerHealth;
        public float minEnemySpawnDelay;
        public float maxEnemySpawnDelay;
        public int enemiesToWin;
        public float enemyMoveSpeed;
        public float enemyHealth;
        public float distanceForKillPlayer;
        public int goldPerEnemy;
    }
}