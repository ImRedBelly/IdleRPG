using System;

namespace Core.Services
{
    public enum GameState
    {
        Menu,
        Playing,
        Victory,
        Defeat
    }

    public class GameStateService
    {
        public event Action<GameState> OnChangeGameState;

        private GameState _currentState;

        public void StartGame()
        {
            _currentState = GameState.Playing;
            OnChangeGameState?.Invoke(_currentState);
        }

        public void Victory()
        {
            _currentState = GameState.Victory;
            OnChangeGameState?.Invoke(_currentState);
        }

        public void GameOver()
        {
            _currentState = GameState.Defeat;
            OnChangeGameState?.Invoke(_currentState);
        }

        public void ExitToMenu()
        {
            _currentState = GameState.Menu;
            OnChangeGameState?.Invoke(_currentState);
        }
    }
}