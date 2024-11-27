using UnityEngine;

namespace Core.Characters.Interfaces
{
    public interface IPlayer : IDamagable
    {
        Vector3 Position { get; }
    }
}