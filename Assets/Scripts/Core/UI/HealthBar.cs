using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        
        public void SetHealth(float health)
        {
            slider.value = health;
        }
    }
}