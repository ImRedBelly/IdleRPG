using Zenject;
using UnityEngine;
using Core.Services;
using UnityEngine.UI;

namespace Core.UI
{
    public class PlayGamePopup : MonoBehaviour
    {
        [SerializeField] private Button buttonPlayGame;
        [Inject] private GameStateService _gameStateService;

        private void OnEnable() => buttonPlayGame.onClick.AddListener(OnPlayGame);
        private void OnDisable() => buttonPlayGame.onClick.RemoveListener(OnPlayGame);
        private void OnPlayGame() => _gameStateService.StartGame();
    }
}