using System;
using Core.Services.Interfaces;

namespace Core.Services
{
    public class GoldService : IGoldService
    {
        public event Action<int> OnChangeGold;


        private int _currentGold;

        public void AddGold(int gold)
        {
            _currentGold += gold;
            OnChangeGold?.Invoke(_currentGold);
        }
    }
}