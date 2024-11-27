using System;

namespace Core.Services.Interfaces
{
    public interface IGoldService
    {
        public event Action<int> OnChangeGold;
        public void AddGold(int gold);
    }
}