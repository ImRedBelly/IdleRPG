using Core.Services.Interfaces;
using TMPro;
using Zenject;
using UnityEngine;

namespace Core.UI
{
    public class GoldCounterViewer : MonoBehaviour
    {
        [SerializeField] private TMP_Text goldText;

        private IGoldService _goldService;

        [Inject]
        private void Construct(IGoldService goldService)
        {
            _goldService = goldService;
            _goldService.OnChangeGold += SetGoldText;
            SetGoldText(0);
        }

        private void OnDisable()
        {
            _goldService.OnChangeGold -= SetGoldText;
        }

        private void SetGoldText(int goldValue)
        {
            goldText.text = "Gold: " + goldValue;
        }
    }
}