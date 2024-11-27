using Core.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.UI
{
    public class DefeatPopup : MonoBehaviour
    {
        [SerializeField] private Button buttonGoToMenu;
        [Inject] private GameStateService _gameStateService;

        private void OnEnable() => buttonGoToMenu.onClick.AddListener(GoToMenu);
        private void OnDisable() => buttonGoToMenu.onClick.RemoveListener(GoToMenu);
        private void GoToMenu() => _gameStateService.ExitToMenu();
    }
}