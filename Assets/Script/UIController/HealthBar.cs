using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;
    void Awake()
    {
        instance =this;
    }
    [SerializeField] Slider slider;
    public void SetHealth(int health)
    {
        slider.value = health;
    }
    public void SetMaxValue(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }
}
