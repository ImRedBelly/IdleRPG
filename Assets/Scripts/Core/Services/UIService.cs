using Zenject;
using Core.UI;
using UnityEngine;

namespace Core.Services
{
    public class UIService : MonoBehaviour
    {
        [SerializeField] PlayGamePopup playGamePopup;
        [SerializeField] VictoryPopup victoryPopup;
        [SerializeField] DefeatPopup defeatPopup;

        [Inject] private GameStateService _gameStateService;

        private void Start()
        {
            _gameStateService.OnChangeGameState += ChangeGameState;
            ChangeGameState(GameState.Menu);
        }

        private void OnDisable()
        {
            _gameStateService.OnChangeGameState -= ChangeGameState;
        }

        private void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Menu:
                    playGamePopup?.gameObject.SetActive(true);
                    victoryPopup?.gameObject.SetActive(false);
                    defeatPopup?.gameObject.SetActive(false);
                    break;
                case GameState.Playing:
                    playGamePopup?.gameObject.SetActive(false);
                    victoryPopup?.gameObject.SetActive(false);
                    defeatPopup?.gameObject.SetActive(false);
                    break;
                case GameState.Victory:
                    victoryPopup?.gameObject.SetActive(true);
                    break;
                case GameState.Defeat:
                    defeatPopup?.gameObject.SetActive(true);
                    break;
            }
        }
    }
}