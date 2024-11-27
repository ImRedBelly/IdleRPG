using Core.Characters.Interfaces;
using UnityEngine;

namespace Core.Services.Interfaces
{
    public interface IEnemyService
    {
        IDamagable GetClosestEnemy(Vector3 position);
    }
}