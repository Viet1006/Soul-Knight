using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] PlayerHandleEffect playerHandleEffect;
    void Start()
    {
        playerHandleEffect.OnHealthChange += SetHealth;
        SetMaxValue(playerHandleEffect.heroData.health);
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
    public void SetMaxValue(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }
    void OnDestroy()
    {
        playerHandleEffect.OnHealthChange -= SetHealth;
    }
}
